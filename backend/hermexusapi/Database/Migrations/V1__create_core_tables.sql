-- Migration: Create Core Tables (users, companies, roles, permissions, bot_settings, whatsapp_contacts)

CREATE TABLE IF NOT EXISTS `users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `username` VARCHAR(100) NOT NULL,
  `refresh_token` VARCHAR(255) NULL,
  `refresh_token_expiry_time` TIMESTAMP NULL,
  `name` VARCHAR(150) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `username_UNIQUE` (`username`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `companies` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `name` VARCHAR(150) NOT NULL,
  `legal_name` VARCHAR(150) NOT NULL,
  `document_number` CHAR(30) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `document_number_UNIQUE` (`document_number`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `roles` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `description` VARCHAR(150) NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `name_UNIQUE` (`name`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sectors` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `company_id` INT NOT NULL,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `name` VARCHAR(100) NOT NULL,
  `description` VARCHAR(150) NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `description` VARCHAR(150) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `whatsapp_contacts` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(120) NULL,
  `phone_number` VARCHAR(20) NOT NULL,
  `wa_id` VARCHAR(30) NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `phone_number_UNIQUE` (`phone_number`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `bot_settings` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `name` VARCHAR(150) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
) ENGINE = InnoDB;