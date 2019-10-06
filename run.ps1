try{

Stop-Transcript
}
Catch
{
	Stop-Transcript
    write-host $_.Exception.Message;

}