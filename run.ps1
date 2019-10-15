try{
	$PROJECTNAME = 'Automation'
	$DateTime = get-date -format ddd_dd.MM.yyyy_HH.mm.ss
	$RESULTDIR="C:\Automation\$DateTime"
	
	Start-Transcript -path $RESULTDIR\logs\log_${DateTime}_$environment.log
	
	$RESULTXML="$RESULTDIR\$DATEYYYYMMDD"+"TestResult"+"$ENVIRONMENT.xml"
	Set-Location $PSScriptRoot
	

	$listDirectories = Get-ChildItem -Path .\packages -Include tools* -Recurse -Directory | Select-Object FullName
	foreach($directory in $listDirectories.FullName) {
		$env:Path+=";"+$directory
	}
	
	$outDir = "$PSScriptRoot\$PROJECTNAME\bin\debug"
	$PROJECT="$PSScriptRoot\$PROJECTNAME\$PROJECTNAME.csproj"
	$TESTSELECT = 'cat == PERSONAL && cat == MEMBERSHIP'
	$RESULOUTTXT="$RESULTDIR\$DATEYYYYMMDD"+"TestResult.txt"
    $OUTHTML = "$RESULTDIR\$ENVIRONMENT_${DateTime}.html"
    $RESULTLOG="$RESULTDIR\$DATEYYYYMMDD"+"TestResult"+"$ENVIRONMENT.log"

	
	$tests = (Get-ChildItem $outDir -Recurse -Include Automation.dll)
	
	$OUTPUT  = nunit3-console --out=$RESULOUTTXT --framework=net-4.5 --result="$RESULTXML;format=nunit2" $tests --where "$TESTSELECT"
	
	$OUTPUT | Out-File $RESULTLOG 
    Write-Host $OUTPUT -Separator "`n"
    $TESTRESULTS = $OUTPUT | Select-String 'Test Count'
    $DURATION = $OUTPUT | Select-String 'Duration: '

    (Get-Content $RESULOUTTXT) | ForEach-Object { $_ -replace '=>', '*****' } | Set-Content $RESULOUTTXT

    #specflow nunitexecutionreport --ProjectFile=$PROJECT --xmlTestResult=$RESULTXML --testOutput=$RESULOUTTXT --OutputFile=$OUTHTML 
    
    ReportUnit $RESULTXML

	Stop-Transcript 
}
Catch
{	
Stop-Transcript
    write-host $_.Exception.Message;

}