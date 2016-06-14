#
# DeleteResources.ps1
#
Param(
    [string] [Parameter(Mandatory=$true)] $ResourceGroupName,
    [string] $TemplateFile = '..\Templates\deleteResources.deploy.json'
)
#Write-Output $TemplateFile
Remove-AzureRmResourceGroup -Name $ResourceGroupName
#New-AzureRmResourceGroupDeployment -ResourceGroupName $ResourceGroupName -Mode Complete -TemplateFile $TemplateFile -Force -Verbose
