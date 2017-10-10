using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

namespace ColorMine.Palettes
{
    public class Palette<T> : IPalette<T> where T: IColorSpace, new()
    {
        protected IList<T> nativeColors = new List<T>();
        private IList<Lab> labColors = new List<Lab>();

        public T this[int index]
        {
            get => nativeColors[index];
            set
            {
                nativeColors[index] = value;
                labColors[index] = value.To<Lab>();
            }
        }

        public virtual IColorSpaceComparison ComparisonAlgorithm { get; set; } = new CieDe2000Comparison();

        public int Count => nativeColors.Count;

        public bool IsReadOnly => nativeColors.IsReadOnly;

        public void Add(T item)
        {
            nativeColors.Add(item);
            labColors.Add(item.To<Lab>());
        }

        public void Clear()
        {
            nativeColors.Clear();
            labColors.Clear();
        }


        /// <remarks>
        /// This comparer ought to be overriden by any implementation which also stores colors in another color space.
        /// </remarks>
        public virtual bool Contains(T item)
        {
            return nativeColors.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            nativeColors.CopyTo(array, arrayIndex);
        }

        public struct FindResult<C>
        {
            public int Index;
            public double Distance;
            public C Color;

            public FindResult(int index, double distance, C color)
            {
                Index = index;
                Distance = distance;
                Color = color;
            }
        }

        public FindResult<TColor> FindClosestColor<TColor>(IColorSpace color) where TColor : IColorSpace, new()
        {
            var result = 0;
            var minValue = double.MaxValue;
            for (var index = 0; index < labColors.Count; index++)
            {
                var paletteColor = labColors[index];
                var distance = ComparisonAlgorithm.Compare(color, paletteColor);
                if (distance < minValue)
                {
                    result = index;
                    minValue = distance;
                }
            }

            return new FindResult<TColor>(result, minValue, labColors[result].To<TColor>());
        }

        public T FindClosestColor(IColorSpace color)
        {
            return FindClosestColor<T>(color).Color;
        }
        

        public int FindClosestColorIndex(IColorSpace color)
        {
            return FindClosestColor<T>(color).Index;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return nativeColors.GetEnumerator();
        }

        /// <remarks>
        /// This comparer ought to be overriden by any implementation which also stores colors in another color space.
        /// </remarks>
        public virtual int IndexOf(T item)
        {
            return nativeColors.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            nativeColors.Insert(index, item);
            labColors.Insert(index, item.To<Lab>());
        }

        public bool Remove(T item)
        {


            var index = nativeColors.IndexOf(item);
            if (index >= 0)
            {
                nativeColors.RemoveAt(index);
                labColors.RemoveAt(index);
            }
            return index >= 0;
        }

        public void RemoveAt(int index)
        {
            nativeColors.RemoveAt(index);
            labColors.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nativeColors.GetEnumerator();
        }
    }
}