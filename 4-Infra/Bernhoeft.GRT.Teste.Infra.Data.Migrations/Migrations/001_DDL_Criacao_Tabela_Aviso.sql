CREATE TABLE "Aviso" (
    id SERIAL PRIMARY KEY,
    ativo BOOLEAN NOT NULL,
    titulo VARCHAR(50) NOT NULL,
    mensagem TEXT NOT NULL
);