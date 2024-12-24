$cur_dir        = (Split-Path -Path $MyInvocation.MyCommand.Path)
$solution_file  = Join-Path $cur_dir "Hello.sln"

# 패키지 복원
dotnet restore $solution_file `
    --verbosity q

# 빌드
dotnet build $solution_file `
    --no-restore `
    --configuration Release `
    --verbosity q

# 테스트
dotnet test $solution_file `
    --configuration Release `
    --no-restore `
    --no-build `
    --collect "XPlat Code Coverage" `
    --logger "trx;LogFileName=logs.trx" `
    --verbosity q

# dotnet new tool-manifest
# dotnet tool install dotnet-reportgenerator-globaltool

# dotnet tool install dotnet-reportgenerator-globaltool `
#     --tool-path reportgeneratortool `
#     --version 5.4.1 `
#     --ignore-failed-sources

# reportgenerator `
#     -reports: "${cur_dir}/**/*.cobertura.xml" `
#     -targetdir: "${cur_dir}/.build/coverage" `
#     #-reporttypes:Cobertura;MarkdownSummaryGithub `
#     -reporttypes:Cobertura;MarkdownSummary;MarkdownAssembliesSummary;MarkdownSummaryGithub
#     -assemblyfilters:+* `
#     -classfilters:+* `
#     -filefilters:+* `
#     -riskhotspotassemblyfilters:+* `
#     -riskhotspotclassfilters:+* `
#     -verbosity:Warning `
#     -title:Code Coverage

#     # -sourcedirs: `
#     # -historydir: `
#     # -plugins: `
#     # -tag:36_12463446679 `
#     # -license: