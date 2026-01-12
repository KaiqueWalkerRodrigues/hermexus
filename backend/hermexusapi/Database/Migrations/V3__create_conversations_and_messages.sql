-- Migration: Create Conversations and Messages

CREATE TABLE IF NOT EXISTS `conversations` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sector_id` INT NOT NULL,
  `contact_id` INT NOT NULL,
  `companies_id` INT NULL,
  `platform` TINYINT NOT NULL DEFAULT 1,
  `status` TINYINT NULL,
  `ai_enabled` TINYINT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `messages` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `conversation_id` INT NOT NULL,
  `user_id` INT NULL,
  `contact_id` INT NULL,
  `content` TEXT NOT NULL,
  `type` TINYINT NOT NULL DEFAULT 1,
  `status` TINYINT NOT NULL DEFAULT 0,
  `is_bot` TINYINT NULL,
  `external_id` VARCHAR(30) NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
) ENGINE = InnoDB;