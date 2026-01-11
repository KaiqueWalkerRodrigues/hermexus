-- -----------------------------------------------------
-- Table `hermexus_db`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `username` VARCHAR(100) NOT NULL,
  `refresh_token` VARCHAR(255) NULL DEFAULT NULL,
  `refresh_token_expiry_time` TIMESTAMP NULL DEFAULT NULL,
  `name` VARCHAR(150) NOT NULL,
  `password` VARCHAR(255) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC) VISIBLE)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`companies`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`companies` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `name` VARCHAR(150) NOT NULL,
  `legal_name` VARCHAR(150) NOT NULL,
  `document_number` CHAR(30) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `document_number_UNIQUE` (`document_number` ASC) VISIBLE)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`roles` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `description` VARCHAR(150) NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `description` VARCHAR(150) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`whatsapp_contacts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`whatsapp_contacts` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(120) NULL,
  `phone_number` VARCHAR(20) NOT NULL,
  `wa_id` VARCHAR(30) NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `phone_number_UNIQUE` (`phone_number` ASC) VISIBLE)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`bot_settings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`bot_settings` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `is_active` TINYINT NOT NULL DEFAULT 1,
  `name` VARCHAR(150) NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;