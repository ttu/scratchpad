Function Get-ProjectReferences ($rootFolder)
{
    $blackList = 'Test|Project1|Project2'

    $projectFiles = Get-ChildItem $rootFolder -Filter *.csproj -Recurse
    $ns = @{ defaultNamespace = "http://schemas.microsoft.com/developer/msbuild/2003" }

    $projectInfos = New-Object 'System.Collections.Generic.Dictionary[String,String]'
    $refTexts = New-Object 'System.Collections.Generic.List[String]'

    $refTexts = New-Object 'System.Collections.Generic.List[String]'

    $projectFiles | ForEach-Object {
        $projectFile = $_ | Select-Object -ExpandProperty FullName
        $projectName = $_ | Select-Object -ExpandProperty BaseName

        $projectXml = [xml](Get-Content $projectFile)

        $projectReferences = $projectXml | Select-Xml '//defaultNamespace:ProjectReference/defaultNamespace:Project' -Namespace $ns | Select-Object -ExpandProperty Node | Select-Object -ExpandProperty "#text"

        $realName = $projectXml | Select-Xml '//defaultNamespace:AssemblyName' -Namespace $ns
        $projectId = $projectXml | Select-Xml '//defaultNamespace:ProjectGuid' -Namespace $ns

        $projectInfos.Add($projectId, $realName)

        if ($projectName -notmatch $blackList) {
            $projectReferences | ForEach-Object {
                $refTexts.Add("[" + $projectId + "] -> [" + $_ + "]")
            }
        }  
    }

    $fullText = ''
	$url = 'http://yuml.me/diagram/nofunky;/class/edit/// My Diagram, '
	$nl = [Environment]::NewLine
	
	$refTexts | ForEach-Object {
        $text = $_

        $projectInfos.GetEnumerator() | ForEach-Object {
            $text = $text -replace $_.Key, $_.Value
        }

        $fullText = $fullText + $text + $nl
		$url = $url + $text + ", "
    }
	
	$url
	$nl
	$fullText
}

Get-ProjectReferences "C:\MySolutionFolder" | Out-File ".\References.txt"