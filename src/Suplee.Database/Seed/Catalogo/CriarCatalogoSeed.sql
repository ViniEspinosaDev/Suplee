CREATE PROCEDURE CriarCatalogoSeed AS
BEGIN
	-- Criando as Categorias
	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Vitaminas'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao].[Icone].[Cor]) VALUES (NEWID(), 'Vitaminas', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 1)
	END
	
	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Proteínas'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao].[Icone].[Cor]) VALUES (NEWID(), 'Proteínas', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 6)
	END

	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Minerais'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao].[Icone].[Cor]) VALUES (NEWID(), 'Minerais', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 15)
	END

	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Ômega-3'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao].[Icone].[Cor]) VALUES (NEWID(), 'Ômega-3', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 3)
	END
	-- END Criando as Categorias

	-- Criando os Efeitos
	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Imunidade'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao].[Icone]) VALUES (NEWID(), 'Imunidade', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Fortalecimento Muscular'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao].[Icone]) VALUES (NEWID(), 'Fortalecimento Muscular', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Relaxante'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao].[Icone]) VALUES (NEWID(), 'Relaxante', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Fortificação Óssea'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao].[Icone]) VALUES (NEWID(), 'Fortificação Óssea', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Vitamina D'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao].[Icone]) VALUES (NEWID(), 'Vitamina D', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Memória'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao].[Icone]) VALUES (NEWID(), 'Memória', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END
	-- END Criando os Efeitos
END