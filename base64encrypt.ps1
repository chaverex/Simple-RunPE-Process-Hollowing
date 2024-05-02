$filePath = "file.exe"

$encryptedFilePath = "C:\Users\Admin\Desktop\base.txt"

$data = Get-Content $filePath -Encoding Byte

if ($data -ne $null) {
    $encryptedData = [Convert]::ToBase64String($data)
    $encryptedBytes = [System.Text.Encoding]::UTF8.GetBytes($encryptedData)
    [System.IO.File]::WriteAllBytes($encryptedFilePath, $encryptedBytes)

    Write-Host "saved to file."
}