@{
  Name = "Light Ayuish"
  Host = @{
    PrivateData = @{
      ErrorForegroundColor    = 'DarkRed'
      ErrorBackgroundColor    = 'White'
      DebugForegroundColor    = 'DarkGreen'
      DebugBackgroundColor    = 'White'
      ProgressForegroundColor = 'Yellow'
      ProgressBackgroundColor = 'DarkMagenta'
      VerboseForegroundColor  = 'DarkBlue'
      VerboseBackgroundColor  = 'White'
      WarningForegroundColor  = 'DarkMagenta'
      WarningBackgroundColor  = 'White'
    }
  }
  ConsoleBackground = 'White'
  ConsoleForeground = 'Black'
  PopupBackground   = 'DarkCyan'
  PopupForeground   = 'Black'
  ConsoleColors = @(
    '#000000'
    '#0073C3'
    '#4A8100'
    '#008E81'
    '#BE0000'
    '#8F0057'
    '#BB6200'
    '#848388'
    '#454545'
    '#12A8CD'
    '#81B600'
    '#2BC2A7'
    '#CA7073'
    '#C05478'
    '#CC9800'
    '#FFFFFF'
  )
  PSReadLine = @{
    Colors = @{
      Comment            = 'Gray'
      Operator           = 'Red'
      Member             = 'Magenta'
      Command            = 'Black'
      Parameter          = 'DarkGray'
      Number             = 'DarkRed'
      Selection          = '[100m[96m'
      Variable           = 'DarkMagenta'
      Type               = 'DarkBlue'
      Keyword            = 'DarkYellow'
      Default            = 'DarkGray'
      ContinuationPrompt = 'Yellow'
      Emphasis           = 'MediumVioletRed'
      Error              = 'Red'
      String             = 'DarkCyan'
    }
  }
}
