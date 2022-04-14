variable "subscription_id" {
  type        = string
  description = "Azure subscription id"
}

variable "tenant_id" {
  type        = string
  description = "Azure tenant id"
}

variable "client_id" {
  type        = string
  description = "Azure service principal (app registration) client id"
}

variable "client_secret" {
  type        = string
  description = "Azure service principal (app registration) client secret"
}