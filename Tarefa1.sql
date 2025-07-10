DROP TABLE IF EXISTS Pessoa;
DROP TABLE IF EXISTS Departamento;

CREATE TABLE Departamento (
	Id INT PRIMARY KEY,
	Nome VARCHAR(10) NOT NULL
);

CREATE TABLE Pessoa (
	Id INT PRIMARY KEY,
	Nome VARCHAR(10) NOT NULL,
	Salario NUMERIC(19, 2) NOT NULL,
	DeptId INT NOT NULL,
	FOREIGN KEY (DeptId) REFERENCES Departamento (Id)
);

INSERT INTO Departamento
	(Id, Nome)
VALUES
	(1, 'TI'),
	(2, 'Vendas');
	
INSERT INTO Pessoa
	(Id, Nome, Salario, DeptId)
VALUES
	(1, 'Joe', 70000, 1),
	(2, 'Henry', 80000, 2),
	(3, 'Sam', 60000, 2),
	(4, 'Max', 90000, 1);
	
SELECT
	Departamento.Nome [Departamento],
	MIN (Pessoa.Nome) [Pessoa],
	Max (Pessoa.Salario) [Salario]
FROM 
	Pessoa
JOIN
	Departamento
ON
	Pessoa.DeptId = Departamento.Id
GROUP BY
	Departamento.Nome;
	
DROP TABLE IF EXISTS Pessoa;
DROP TABLE IF EXISTS Departamento;	