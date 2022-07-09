CREATE PROCEDURE CriarCatalogoSeed AS
BEGIN
	-- Criando as Categorias
	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Vitaminas'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao],[Icone],[Cor]) VALUES ('13da56cc-5122-4392-8766-de930231f770', 'Vitaminas', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 1)
	END
	
	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Proteínas'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao],[Icone],[Cor]) VALUES ('8a11a207-806b-4381-a66d-312380079800', 'Proteínas', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 6)
	END

	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Minerais'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao],[Icone],[Cor]) VALUES ('083a4576-f55b-4766-bf50-eb86db16b766', 'Minerais', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 15)
	END

	IF(NOT EXISTS(SELECT 1 FROM [Categoria] WHERE Nome = 'Ômega-3'))
	BEGIN
		INSERT INTO [dbo].[Categoria] ([Id],[Nome],[Descricao],[Icone],[Cor]) VALUES ('06c8e04e-6186-4348-b2b3-c92956593d94', 'Ômega-3', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png', 3)
	END
	-- END Criando as Categorias

	-- Criando os Efeitos
	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Imunidade'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao],[Icone]) VALUES ('8ebad980-9591-41f3-9d87-d4ef541899f5', 'Imunidade', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Fortalecimento Muscular'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao],[Icone]) VALUES ('fd02f052-3595-4609-8b5b-7a92fb653454', 'Fortalecimento Muscular', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Relaxante'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao],[Icone]) VALUES ('fc63bca1-9229-4d6d-a47a-f963f665f1d2', 'Relaxante', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Fortificação Óssea'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao],[Icone]) VALUES ('a6e4f847-ae7d-4d82-84d4-74e5fcaa8057', 'Fortificação Óssea', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Vitamina D'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao],[Icone]) VALUES ('ce648c1c-c4be-42be-a523-1d3eb73e531c', 'Vitamina D', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END

	IF(NOT EXISTS(SELECT 1 FROM [Efeito] WHERE Nome = 'Memória'))
	BEGIN
		INSERT INTO [dbo].[Efeito] ([Id],[Nome],[Descricao],[Icone]) VALUES ('fe9ff416-74cc-4deb-b19e-af2a0fe5617e', 'Memória', 'Uma bela descrição foda', 'https://cdn-icons-png.flaticon.com/512/74/74472.png')
	END
	-- END Criando os Efeitos
END