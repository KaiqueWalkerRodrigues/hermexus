-- Migration: Add Foreign Keys and Indexes (Versão Consolidada)

-- 1. Relacionamentos de Role/Permission
ALTER TABLE `role_permissions` 
ADD CONSTRAINT `fk_role_permissions_permissions` FOREIGN KEY (`permission_id`) REFERENCES `permissions` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_role_permissions_roles` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE;

-- 2. Relacionamentos de User Roles/Permissions
ALTER TABLE `users_roles` 
ADD CONSTRAINT `fk_users_roles_roles` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_users_roles_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;

ALTER TABLE `users_permissions` 
ADD CONSTRAINT `fk_users_permissions_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_users_permissions_permissions` FOREIGN KEY (`permission_id`) REFERENCES `permissions` (`id`) ON DELETE CASCADE;

-- 3. Relacionamentos de Setores (Empresas, Permissões e Usuários)
ALTER TABLE `sectors` 
ADD CONSTRAINT `fk_sectors_companies` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`) ON DELETE CASCADE;

ALTER TABLE `sectors_permissions`
ADD CONSTRAINT `fk_sectors_permissions_permissions` FOREIGN KEY (`permission_id`) REFERENCES `permissions` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_sectors_permissions_sectors` FOREIGN KEY (`sector_id`) REFERENCES `sectors` (`id`) ON DELETE CASCADE;

ALTER TABLE `users_sectors`
ADD CONSTRAINT `fk_users_sectors_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_users_sectors_sectors` FOREIGN KEY (`sector_id`) REFERENCES `sectors` (`id`) ON DELETE CASCADE;

-- 4. Conversas e Mensagens
ALTER TABLE `conversations` 
ADD CONSTRAINT `fk_conversations_companies` FOREIGN KEY (`company_id`) REFERENCES `companies` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_conversations_sectors` FOREIGN KEY (`sector_id`) REFERENCES `sectors` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_conversations_robots` FOREIGN KEY (`robot_id`) REFERENCES `robots` (`id`) ON DELETE CASCADE;

ALTER TABLE `messages` 
ADD CONSTRAINT `fk_messages_conversations` FOREIGN KEY (`conversation_id`) REFERENCES `conversations` (`id`) ON DELETE CASCADE,
ADD CONSTRAINT `fk_messages_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;