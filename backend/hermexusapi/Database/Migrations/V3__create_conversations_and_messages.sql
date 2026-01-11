-- -----------------------------------------------------
-- Table `hermexus_db`.`conversations`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`conversations` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `role_id` INT NOT NULL,
  `contact_id` INT NOT NULL,
  `companies_id` INT NULL,
  `platform` TINYINT NOT NULL DEFAULT 1,
  `status` TINYINT NULL,
  `ai_enabled` TINYINT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_conversations_roles_idx` (`role_id` ASC) VISIBLE,
  INDEX `fk_conversations_companies_idx` (`companies_id` ASC) VISIBLE,
  CONSTRAINT `fk_conversations_roles`
    FOREIGN KEY (`role_id`)
    REFERENCES `hermexus_db`.`roles` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_conversations_companies`
    FOREIGN KEY (`companies_id`)
    REFERENCES `hermexus_db`.`companies` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`messages`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`messages` (
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
  PRIMARY KEY (`id`),
  INDEX `fk_messages_conversations_idx` (`conversation_id` ASC) VISIBLE,
  INDEX `fk_messages_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_messages_conversations`
    FOREIGN KEY (`conversation_id`)
    REFERENCES `hermexus_db`.`conversations` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_messages_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `hermexus_db`.`users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;