SELECT nevezes.Id, 
	   nevezes.BoraszId, 
	   nevezes.FantaziaNev, 
	   nevezes.Borvidek, 
	   nevezes.Evjarat, 
	   nevezes.KategoriaId, 
	   nevezes.Helyezes, 
	   kategoria.Megnevezes AS KategoriaNev 
FROM nevezes 
JOIN kategoria ON nevezes.kategoriaid = kategoria.id 
WHERE 1 = 1