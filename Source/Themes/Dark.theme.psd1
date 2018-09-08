@{
  Name = "Dark Monokaish"
  Host = @{
    PrivateData = @{
      ErrorForegroundColor    = 'Red'
      ErrorBackgroundColor    = 'Black'
      DebugForegroundColor    = 'Green'
      DebugBackgroundColor    = 'Black'
      ProgressForegroundColor = 'Yellow'
      ProgressBackgroundColor = 'DarkMagenta'
      VerboseForegroundColor  = 'Cyan'
      VerboseBackgroundColor  = 'Black'
      WarningForegroundColor  = 'Yellow'
      WarningBackgroundColor  = 'Black'
    }
  }
  ConsoleBackground = 'Black'
  ConsoleForeground = 'White'
  PopupBackground   = 'DarkCyan'
  PopupForeground   = 'Black'
  ConsoleColors = @(
      '#212021'
      '#01A0E4'
      '#01A252'
      '#55C4CF'
      '#D92D20'
      '#A16A94'
      '#FBED02'
      '#A5A2A2'
      '#493F3F'
      '#6ECEFF'
      '#6CD18E'
      '#95F2FF'
      '#FF6E6D'
      '#D29BC6'
      '#FFFF85'
      '#FFFCFF'
    )
  PSReadLine = @{
    Colors = @{
      Comment            = 'Gray'
      Operator           = 'Red'
      Member             = 'Magenta'
      Command            = 'DarkCyan'
      Parameter          = 'Cyan'
      Number             = 'DarkRed'
      Selection          = '[100m[96m'
      Variable           = 'DarkMagenta'
      Type               = 'DarkBlue'
      Keyword            = 'Yellow'
      Default            = 'White'
      ContinuationPrompt = 'Yellow'
      Emphasis           = 'MediumVioletRed'
      Error              = 'Red'
      String             = 'Green'
    }
  }
}
