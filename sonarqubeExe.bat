dotnet sonarscanner begin /k:".netcoreWebapi" /d:sonar.host.url="http://localhost:9000" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.login="sqp_201e37773ce66f074fc409774e1351e0325d3b2b"
dotnet build --no-incremental
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
dotnet sonarscanner end /d:sonar.login="sqp_201e37773ce66f074fc409774e1351e0325d3b2b"