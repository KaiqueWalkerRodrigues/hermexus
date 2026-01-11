-- Constraints que faltaram (baseado no seu schema original)
ALTER TABLE `conversations`
ADD CONSTRAINT `fk_conversations_whatsapp_contacts`
FOREIGN KEY (`contact_id`)
REFERENCES `whatsapp_contacts` (`id`)
ON DELETE NO ACTION
ON UPDATE NO ACTION;

ALTER TABLE `messages`
ADD CONSTRAINT `fk_messages_whatsapp_contacts`
FOREIGN KEY (`contact_id`)
REFERENCES `whatsapp_contacts` (`id`)
ON DELETE NO ACTION
ON UPDATE NO ACTION;

-- Índices adicionais para performance
CREATE INDEX `idx_conversations_contact` ON `conversations` (`contact_id`);
CREATE INDEX `idx_messages_contact` ON `messages` (`contact_id`);
CREATE INDEX `idx_messages_created` ON `messages` (`created_at`);