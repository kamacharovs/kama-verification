terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.1.0"
    }

    random = {
      source = "hashicorp/random"
      version = "3.0.1"
    }
  }

  cloud {
    organization = "kamacharovs"

    workspaces {
      name = "kama-verification"
    }
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  tenant_id       = var.tenant_id
  client_id       = var.client_id
  client_secret   = var.client_secret

  features {}
}