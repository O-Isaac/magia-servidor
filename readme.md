[Crear Servidor](#-como-creo-el-servidor-el-servidor) / [Archivos de configuracion](#-archivos-default-origin-github) / [Api](#-2º-parte---crear-funcion-lambad) / [Nota](#-pequeña-anotacion)

# 🤔 ¿Como creo el servidor el servidor?
Para crear el servidor necesitas tener docker y docker-compose instalado,
para eso te recomiendo que sigas esta [documentacion](https://docs.docker.com/desktop/windows/install/)
esta en ingles 🇺🇸 no creo que sea un problema para ti.

# 📚 Archivos default, origin, github
`config/*.conf`

Estos son servidores de donde se obtendra los archivos para el juego:
- **defaul** servidor bridge que se encarga de comunicar los servidores con un load balance.
- **origin** servidor proxy reverso que apunta al host del juego.
- **github** servidor proxy reverso que apunta al repositorio de github.

Unicamente necesitaras el puerto `80` abierto en tu servidor en la nube, si planeas usarlo en local
te recomiendo que uses dns para modificar el localhost -> dominio, dado que el cliente de rayshift no
reconoce el localhost como un servidor valido (no se el motivo)


# 📕 2º Parte - Crear funcion lambad
Hay que hacer que el cliente se conecte en el servidor para ello solo tienes que crear una 
funcion lambad o un servidor con express, flask o otro framework/modulo de api, que responda
con la siguiente respuesta:

```json
/**
 * Tambien puedes hacerlo en el mismo servidor pero no lo recomiendo dado que puede interferir ciertas 
 * peticiones del backend del juego con el servidor original 
 *
 * (esto no lo he comprobado pero es una ligera suposicion aun no lo he comprobado 😅).
 **/

{
    "status": 200,
    "response": {
        "endpoint": "<Direcion url del servidor>",
        "version": 110,
        "max_threads": 20
    },
    "message": "ok"
}
```

y luego modificar los strings de los archivos smalis de cliente de rayshift o el suyo propio
remplanzado la direccion del servidor de rayshift con el suyo propio.
`smali/MagiaNative/app/src/main/java/io/kamihama/magianative/RestClient.smali`

# 🧵 Pequeña anotacion
Si quieres saber como crear tu propio cliente visita el repositorio de [magiatranslate](https://github.com/rayshift/magiatranslate) para mas informacion (esta en ingles)
