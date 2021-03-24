
$ErrorActionPreference="Stop"

if (!(Get-Command dotnet -ErrorAction SilentlyContinue))
{
    # Install .NET Core (https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script)
    &powershell -NoProfile -ExecutionPolicy unrestricted -Command "[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; &([scriptblock]::Create((Invoke-WebRequest -UseBasicParsing 'https://dot.net/v1/dotnet-install.ps1'))) -Channel Current"
}

dotnet tool restore
dotnet fake build @Args

exit $LASTEXITCODE;