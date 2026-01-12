-- Migration: Create User Permissions, Roles and Sector Relations

CREATE TABLE IF NOT EXISTS `users_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `idx_users_permissions_user` (`user_id`),
  INDEX `idx_users_permissions_permission` (`permission_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `role_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `role_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `idx_role_permissions_role` (`role_id`),
  INDEX `idx_role_permissions_permission` (`permission_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `users_roles` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `role_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `idx_users_roles_user` (`user_id`),
  INDEX `idx_users_roles_role` (`role_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sectors_permissions` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sector_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `idx_sectors_permissions_sector` (`sector_id`),
  INDEX `idx_sectors_permissions_permission` (`permission_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `users_sectors` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `sector_id` INT NOT NULL,
  `user_id` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `deleted_at` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  INDEX `idx_users_sectors_sector` (`sector_id`),
  INDEX `idx_users_sectors_user` (`user_id`)
) ENGINE = InnoDB;