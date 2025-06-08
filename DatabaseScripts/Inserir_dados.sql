INSERT INTO Usuarios (Nome, Email, PorcentagemCorretagem) VALUES
('João Silva', 'joao@email.com', 0.25),
('Maria Souza', 'maria@email.com', 0.20);

INSERT INTO Ativos (Codigo, Nome) VALUES
('ITUB4', 'Itaú Unibanco'),
('PETR4', 'Petrobras');

INSERT INTO Cotacoes (AtivoId, Preco, DataCotacao) VALUES
(1, 28.50, '2025-06-07'),
(2, 35.80, '2025-06-07');

INSERT INTO Operacoes (UsuarioId, AtivoId, TipoOperacao, Quantidade, PrecoUnitario, Corretagem, DataOperacao) VALUES
(1, 1, 'COMPRA', 100, 27.00, 10.00, '2025-05-01'),
(1, 2, 'COMPRA', 50, 34.00, 8.00, '2025-05-10'),
(2, 1, 'COMPRA', 200, 26.50, 15.00, '2025-05-15');

INSERT INTO Posicoes (UsuarioId, AtivoId, Quantidade, PrecoMedio) VALUES
(1, 1, 100, 27.00),
(1, 2, 50, 34.00),
(2, 1, 200, 26.50);
