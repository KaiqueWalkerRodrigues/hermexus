-- -----------------------------------------------------
-- Table `hermexus_db`.`role_permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`role_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `role_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_role_permissions_permissions_idx` (`permission_id` ASC) VISIBLE,
  INDEX `fk_role_permissions_role_idx` (`role_id` ASC) VISIBLE,
  CONSTRAINT `fk_role_permissions_permissions`
    FOREIGN KEY (`permission_id`)
    REFERENCES `hermexus_db`.`permissions` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_role_permissions_roles`
    FOREIGN KEY (`role_id`)
    REFERENCES `hermexus_db`.`roles` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`users_roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`users_roles` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `role_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_users_roles_roles_idx` (`role_id` ASC) VISIBLE,
  INDEX `fk_users_roles_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_users_roles_roles`
    FOREIGN KEY (`role_id`)
    REFERENCES `hermexus_db`.`roles` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_users_roles_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `hermexus_db`.`users` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `hermexus_db`.`users_permissions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `hermexus_db`.`users_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL,
  `updated_at` TIMESTAMP NOT NULL,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_users_permissions_users_idx` (`user_id` ASC) VISIBLE,
  INDEX `fk_users_permissions_permissions_idx` (`permission_id` ASC) VISIBLE,
  CONSTRAINT `fk_users_permissions_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `hermexus_db`.`users` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_users_permissions_permissions`
    FOREIGN KEY (`permission_id`)
    REFERENCES `hermexus_db`.`permissions` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;