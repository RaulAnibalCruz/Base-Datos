# Proyecto de Bloody Roar 2

<img src="https://et12.edu.ar/imgs/computacion/vamoaprogramabanner.png">



## DER

```mermaid
erDiagram
    Usuario {
        INT idUsuario PK
        VARCHAR Nombre
        CHAR Contrasenia
        VARCHAR Email
    }

    Personaje {
        INT idPersonaje PK
        VARCHAR Nombre
        VARCHAR NombreBestia
        INT ResistenciaBestia
        INT Ataque_idAtaque FK
    }

    Ataque {
        INT idAtaque PK
        VARCHAR Tipo_Ataque
        INT Danio
    }

    ModoJuego {
        INT idModoJuego PK
        VARCHAR Nombre
    }

    Combate {
        INT idCombate PK
        INT idPersonaje FK
        INT idJugador FK
        INT idModo_Juego FK
        INT Duracion
    }

    Usuario ||--o{ Combate : ""
    Personaje ||--o{ Combate : ""
    ModoJuego ||--o{ Combate : ""
    Ataque ||--o| Personaje : ""


```
## Comenzando 

Clonar el repositorio github, desde Github Desktop o ejecutar en la terminal o CMD:

```
git clone https://github.com/giovanni-mendez/Base-Datos.git
```

## Requisitos 
- .NET 8.0 SDK
- MySQL 8 


## Integrantes del Proyecto:

* Diego Quintero
* Raul Cruz
* Giovanni Mendez
