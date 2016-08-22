param(
	[string]$Path = (Get-Location).ToString()
)


function Get-MyChildItem
{
  param
  (
    [Parameter(Mandatory = $true)]
    $Path,
    
    $Filter = '*',
    
    [System.Int32]
    $MaxDepth = 3,
    
    [System.Int32]
    $Depth = 0
  )
  
  $Depth++

  Get-ChildItem -Path $Path -Filter $Filter -File 
  
  if ($Depth -le $MaxDepth)
  {
    Get-ChildItem -Path $Path -Directory |
      ForEach-Object { Get-MyChildItem -Path $_.FullName -Filter $Filter -Depth $Depth -MaxDepth $MaxDepth}
  }
  
}

# # run DNU restore on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools

Write-Host "*** Using $Path as the Working Folder" -ForegroundColor Yellow

$projectDefinitions = Get-MyChildItem -Path $Path -Filter 'project.json' -MaxDepth 2

Write-Host "*** The script found the following project.json files:" -ForegroundColor Blue

ForEach ($definition in $projectDefinitions)
{
	#& dnu restore $definition.FullName 2>1
	Write-Host "*** $($definition.FullName)"
}

ForEach ($definition in $projectDefinitions)
{
	& dnu restore $definition.FullName 2>1
}