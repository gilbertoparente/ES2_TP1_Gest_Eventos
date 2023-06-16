CREATE TABLE eventos (
                         id SERIAL PRIMARY KEY,
                         nome VARCHAR(100),
                         data_inicio DATE,
                         data_fim DATE,
                         local VARCHAR(100)
);