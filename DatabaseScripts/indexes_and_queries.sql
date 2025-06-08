CREATE INDEX idx_operacoes_usuario_ativo_data
ON operacoes (usuario_id, ativo_id, data_hora);

SELECT *
FROM operacoes
WHERE usuario_id = @idUsuario
  AND ativo_id = @idAtivo
  AND data_hora >= NOW() - INTERVAL 30 DAY;
  
  -- Atualiza todas as posições com base na nova cotação
-- Desativa o safe mode
SET SQL_SAFE_UPDATES = 0;

UPDATE posicoes p
JOIN cotacoes c ON p.ativo_id = c.ativo_id
SET p.pnl = (c.preco_unitario - p.preco_medio) * p.quantidade
WHERE c.data_hora = (SELECT MAX(c2.data_hora)
                     FROM cotacoes c2
                     WHERE c2.ativo_id = p.ativo_id);
                     
-- Ativa o safe mode                     
SET SQL_SAFE_UPDATES = 1;

