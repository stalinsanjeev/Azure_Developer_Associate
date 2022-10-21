
Connect-AzAccount

$ResourceGroupName ="app-grp"
$IdentityName="app-identity"

New-AzUserAssignedIdentity -ResourceGroupName $ResourceGroupName -Name $IdentityName -Location "North Europe"

$Identity=Get-AzResource -Name $IdentityName -ResourceGroupName $ResourceGroupName
$ResourceId=$Identity.Id
$VmName="vm1"

$vm = Get-AzVM -ResourceGroupName $ResourceGroupName -Name $VmName
Update-AzVM -ResourceGroupName $ResourceGroupName -VM $vm -IdentityType UserAssigned `
-IdentityID $ResourceId