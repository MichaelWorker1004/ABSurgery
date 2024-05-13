$ipAddress = "10.102.0.36" # Replace with the IP address you want to add
$hostname = "sqlabs01.privatelink.database.windows.net" # Replace with the hostname you want to add

$hostsPath = "$env:SystemRoot\system32\drivers\etc\hosts" # Path to the hosts file

# Check if the entry already exists in the hosts file
if (Select-String -Path $hostsPath -Pattern "^$ipAddress\s+$hostname\s*$") {
    Write-Host "Entry already exists in hosts file"
}
else {
    # Append the new entry to the hosts file
    "$ipAddress`t$hostname" | Out-File -FilePath $hostsPath -Encoding utf8 -Append
    Write-Host "Entry added to hosts file"
}