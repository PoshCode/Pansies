#requires -Modules Pansies
using namespace System
using namespace System.IO
using namespace System.Collections
using namespace System.Collections.Generic
using namespace System.Linq
class HslEnumerator : IEnumerator[PoshCode.Pansies.RgbColor] {
  hidden [PoshCode.Pansies.RgbColor]$Start = "White"
  [int]$HueStep = 1
  [int]$LightStep = 1
  [int]$SatStep = 1

  # Default constructor so we can cast hashtables
  HslEnumerator() {}

  HslEnumerator([PoshCode.Pansies.RgbColor]$Start) {
    $this.Start = $Start
  }

  # Implement IEnumerator.
  [PoshCode.Pansies.RgbColor]$Current = $null

  [object] get_Current() {
    return $this.Current
  }

  [bool] MoveNext() {
    if ($null -eq $this.Current) {
      $this.Current = $this.Start
      return $true
    }

    $next = $this.Current.ToHsl();
    $next.H += $this.HueStep;
    $next.H %= 360;

    if ($next.S + $this.SatStep -gt 100) {
      $next.S = 20;
    } else {
      $next.S += $this.SatStep;
    }
    if ($next.L + $this.LightStep -gt 80) {
      $next.L = 20;
    } else {
      $next.L += $this.LightStep
    }

    $this.Current = $next.ToRgb();

    return $true;
  }

  [void] Reset() {
    $this.Current = $null;
  }

  # IDisposable is required by IEnumerator
  [void] Dispose() {}
}