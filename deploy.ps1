$ftpServer = "site77619.siteasp.net"
$ftpUser = "site77619"
$ftpPass = "Xd5_@Lh6B8#c"
$localPath = "E:\Project\GoBite\publish"
$remotePath = "/"

$files = Get-ChildItem -Path $localPath -Recurse -File

$webclient = New-Object System.Net.WebClient
$webclient.Credentials = New-Object System.Net.NetworkCredential($ftpUser, $ftpPass)

foreach ($file in $files) {
    $relativePath = $file.FullName.Substring($localPath.Length + 1)
    $remoteFile = "ftp://$ftpServer/$($relativePath.Replace('\', '/'))"

    try {
        $webclient.UploadFile($remoteFile, $file.FullName)
        Write-Host "OK: $relativePath"
    }
    catch {
        $dirPath = [System.IO.Path]::GetDirectoryName($remoteFile)
        try {
            $webclient.UploadFile($remoteFile, $file.FullName)
            Write-Host "OK (retry): $relativePath"
        }
        catch {
            Write-Host "FAIL: $relativePath - $_"
        }
    }
}

$webclient.Dispose()
Write-Host "`nDone!"
