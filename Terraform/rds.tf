# resource "azurerm_postgresql_server" "kama_verification_rds" {
#   name                = "kama-verification-rds-server"
#   location            = azurerm_resource_group.kama_verification_rg.location
#   resource_group_name = azurerm_resource_group.kama_verification_rg.name

#   administrator_login          = "psqladmin"
#   administrator_login_password = "H@Sh1CoR3!"

#   sku_name   = var.env == "prod" ? "GP_Gen5_4" : "B_Gen4_1"
#   version    = "11"
#   storage_mb = var.env == "prod" ? 640000 : 5120

#   backup_retention_days        = 7
#   geo_redundant_backup_enabled = true
#   auto_grow_enabled            = true

#   public_network_access_enabled    = false
#   ssl_enforcement_enabled          = true
#   ssl_minimal_tls_version_enforced = "TLS1_2"
# }