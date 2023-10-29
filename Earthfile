VERSION 0.7
IMPORT github.com/poshcode/tasks
FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /project

ARG --global EARTHLY_BUILD_SHA
ARG --global EARTHLY_GIT_BRANCH
# These are my common paths, used in my shared /Tasks repo
ARG --global OUTPUT_ROOT=/output
ARG --global TEST_ROOT=/tests
ARG --global TEMP_ROOT=/temp
# These are my common build args, used in my shared /Tasks repo
ARG --global MODULE_NAME=Pansies
ARG --global CONFIGURATION=Release
ARG --global PSMODULE_PUBLISH_KEY
ARG --global NUGET_API_KEY

deps:
    # Dotnet tools and scripts installed by PSGet
    ENV PATH=$HOME/.dotnet/tools:$HOME/.local/share/powershell/Scripts:$PATH
    RUN mkdir /Tasks
    # I'm using Invoke-Build tasks from this other repo which rarely changes
    COPY tasks+tasks/* /Tasks
    # Dealing with dependencies first allows docker to cache packages for us
    # So the dependency cach only re-builds when you add a new dependency
    COPY RequiredModules.psd1 .
    COPY *.csproj .
    RUN ["pwsh", "--file", "/Tasks/_Bootstrap.ps1", "-RequiredModulesPath", "RequiredModules.psd1"]
    # Install-RequiredModule does not support pre-release modules
    RUN ["pwsh", "--command", "Update-Module", "ModuleBuilder", "-AllowPreRelease"]

build:
    FROM +deps
    RUN mkdir $OUTPUT_ROOT $TEST_ROOT $TEMP_ROOT
    COPY . .
    # make sure you have bin and obj in .earthlyignore, as their content from context might cause problems
    RUN ["pwsh", "--command", "Invoke-Build", "-Task", "Build", "-File", "Build.build.ps1"]

    # SAVE ARTIFACT [--keep-ts] [--keep-own] [--if-exists] [--force] <src> [<artifact-dest-path>] [AS LOCAL <local-path>]
    SAVE ARTIFACT $OUTPUT_ROOT/$MODULE_NAME AS LOCAL ./output/$MODULE_NAME

test:
    FROM +build
    COPY . .
    # make sure you have bin and obj in .earthlyignore, as their content from context might cause problems
    RUN ["pwsh", "--command", "Invoke-Build", "-Task", "Test", "-File", "Build.build.ps1"]

    # SAVE ARTIFACT [--keep-ts] [--keep-own] [--if-exists] [--force] <src> [<artifact-dest-path>] [AS LOCAL <local-path>]
    SAVE ARTIFACT $TEST_ROOT AS LOCAL ./output/tests
# runtime:
#     FROM mcr.microsoft.com/dotnet/aspnet:7.0
#     WORKDIR /app
#     COPY +build/output .
#     ENTRYPOINT ["dotnet", "ContainerApp.WebApp.dll"]
#     SAVE IMAGE --push containerapp-webapp:earthly

publish:
    FROM +build
    RUN ["pwsh", "--command", "Invoke-Build", "-Task", "Publish", "-File", "Build.build.ps1"]
    SAVE ARTIFACT $OUTPUT_ROOT/publish AS LOCAL ./output/publish
