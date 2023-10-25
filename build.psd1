@{
    ModuleManifest           = "./Source/Pansies.psd1"
    # The rest of the paths are relative to the manifest
    OutputDirectory          = ".."
    VersionedOutputDirectory = $true
    CopyDirectories          = @('../assemblies')
}
