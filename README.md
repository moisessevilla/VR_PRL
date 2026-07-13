# VR_PRL

### Simulador de Realidad Virtual para la Prevención de Riesgos Laborales

![Unity](https://img.shields.io/badge/Unity-6-black?logo=unity)
![Platform](https://img.shields.io/badge/Platform-Meta%20Quest%203-1c1e21)
![SDK](https://img.shields.io/badge/SDK-Meta%20XR%20SDK-0081FB)
![Status](https://img.shields.io/badge/Estado-En%20desarrollo-yellow)

VR_PRL es un simulador de realidad virtual desarrollado para Meta Quest 3 orientado a la formación en prevención de riesgos laborales dentro de un entorno industrial. El usuario se equipa con los equipos de protección individual correspondientes y recorre una nave industrial abandonada enfrentándose a seis riesgos laborales distintos, cada uno con su propia mecánica de interacción.

Este proyecto es el desarrollo central de mi Trabajo Fin de Máster en el Máster en Industria 4.0 de la Universidad Internacional de Valencia (VIU), bajo la tutorización de Salva Aparici Martínez.

## Índice

- [Contexto](#contexto)
- [El escenario](#el-escenario)
- [Los seis riesgos](#los-seis-riesgos)
- [Características principales](#características-principales)
- [Tecnología](#tecnología)
- [Scripts principales](#scripts-principales)
- [Requisitos](#requisitos)
- [Instalación y ejecución](#instalación-y-ejecución)
- [Estado del proyecto y limitaciones](#estado-del-proyecto-y-limitaciones)
- [Hoja de ruta](#hoja-de-ruta)
- [Créditos y recursos](#créditos-y-recursos)
- [Licencia](#licencia)
- [Autor](#autor)

## Contexto

Los métodos tradicionales de formación en prevención de riesgos laborales, como el aula o el vídeo, no permiten al trabajador experimentar de forma segura una situación de riesgo real. La realidad virtual, reconocida dentro del paradigma de la Industria 4.0 como una tecnología habilitadora, ofrece una alternativa en la que el usuario puede vivir esa experiencia en primera persona sin exponerse a ningún peligro físico.

VR_PRL nace como respuesta a esa limitación, aplicando el concepto de presencia (la sensación subjetiva de "estar ahí" dentro del entorno virtual) para reforzar el aprendizaje de conductas seguras frente a los riesgos más comunes de un entorno industrial.

## El escenario

El simulador recrea una nave industrial abandonada. Antes de acceder a ella, el usuario pasa por un vestuario inicial donde debe equiparse con los EPIs correspondientes siguiendo una secuencia guiada. Solo una vez completada esta secuencia se habilita el acceso a la nave y a los seis riesgos que la componen.

## Los seis riesgos

| # | Riesgo | Mecánica de interacción |
|---|--------|--------------------------|
| R1 | Suelo resbaladizo | Detección de zona de riesgo por suelo mojado o deslizante |
| R2 | Riesgo de caídas al mismo nivel | Obstáculos en el recorrido que deben identificarse y evitarse |
| R3 | Riesgo eléctrico | Señalización e identificación de un peligro eléctrico junto a maquinaria |
| R4 | Objetos de bloqueo de acceso a zonas | Retirada de cajas y obstáculos que bloquean el paso |
| R5 | Riesgo de incendio | Extinción activa de un fuego con extintor |
| R6 | Riesgo de intoxicación por gases | Inhalación de gas venenoso que provoca alucinaciones, desarrollado en una sala de ilusiones con narrativa propia |

El riesgo R6 es, en términos de desarrollo, el más elaborado de los seis. Tras la intoxicación, el usuario entra en una sala de ilusiones concebida como recurso pedagógico para reforzar su capacidad de mantener el foco en la tarea pese a las distracciones del entorno, en lugar de tratarse de un módulo narrativo independiente de los cinco riesgos anteriores.

## Características principales

- Locomoción continua caminando mediante el sistema de locomoción del Meta XR SDK, priorizando la sensación de presencia frente al teletransporte sobre NavMesh
- Secuencia guiada de equipación de EPIs en el vestuario inicial, con detección individual de cada elemento
- Seis zonas de riesgo independientes, cada una con su propia lógica de activación y resolución
- Sala de ilusiones con teletransportes puntuales controlados por script como único recurso narrativo que rompe la locomoción continua
- Interacción basada en distancia y configuración de colliders en lugar de depender exclusivamente de eventos de colisión, para mayor fiabilidad en el Meta XR SDK

## Tecnología

- Motor: Unity 6
- Integración VR: Meta XR SDK
- Dispositivo objetivo: Meta Quest 3
- Lenguaje: C#
- Modelos 3D: Sketchfab
- Audio: Freesound

## Scripts principales

| Script | Función |
|--------|---------|
| `EPIItem.cs` | Lógica de un elemento de EPI individual y su equipación |
| `SecuenciaEPIs.cs` | Orden y validación de la secuencia completa de equipación en el vestuario |
| `ZonaVestuario.cs` | Control de la zona de vestuario y habilitación del acceso a la nave |
| `RiesgoObjeto.cs` | Comportamiento genérico de un objeto de riesgo interactuable |
| `ZonaEntregaRiesgo.cs` | Activación de un riesgo al entrar el usuario en su zona |
| `ZonaLiberacionRiesgo.cs` | Resolución y liberación de un riesgo una vez superado |
| `SecuenciaRiesgos.cs` | Orquestación del progreso del usuario a través de los seis riesgos |
| `ExtincionFuego.cs` | Mecánica de extinción activa del riesgo de incendio (R5) |
| `SecuenciaR6.cs` | Control de la secuencia narrativa de la sala de ilusiones (R6) |
| `R6_DetectorMascara.cs` | Detección de la recogida y uso de la máscara de gas en R6 |

El código completo y comentado de estos diez scripts está disponible en `Assets/`.

## Requisitos

- Unity 6.x con el módulo Android Build Support
- Meta XR SDK (disponible desde el Package Manager de Unity)
- Meta Quest 3
- Meta Horizon Link (antes Oculus Link o Air Link) para pruebas directas desde el editor sin necesidad de generar un instalable

## Instalación y ejecución

1. Clonar el repositorio

   ```bash
   git clone https://github.com/moisessevilla/VR_PRL.git
   ```

2. Abrir el proyecto con Unity Hub usando la versión de Unity 6 especificada en `ProjectSettings`
3. Verificar que el Meta XR SDK está correctamente importado desde el Package Manager
4. Conectar el Meta Quest 3 al equipo mediante Meta Horizon Link
5. Pulsar Play en el editor de Unity para ejecutar el simulador directamente sobre el dispositivo

## Estado del proyecto y limitaciones

El simulador está plenamente funcional y ha sido validado mediante pruebas informales. Actualmente no existe un archivo instalable (APK) para Meta Quest 3, debido a un fallo de compilación en Gradle causado por un conflicto de espacio de nombres entre módulos del Meta XR SDK, todavía sin resolver. Mientras tanto, la ejecución y las pruebas se realizan directamente desde el editor de Unity mediante Meta Horizon Link, lo que permite completar todas las pruebas funcionales previstas sin necesidad del instalable.

El proyecto tampoco incluye, por el momento, un sistema de evaluación cuantitativa del desempeño del usuario.

## Hoja de ruta

- Resolución del conflicto de Gradle y generación de un instalable independiente
- Validación con usuarios reales más allá de las pruebas informales realizadas hasta ahora
- Sistema de evaluación cuantitativa del desempeño del usuario
- Ampliación a nuevos escenarios y riesgos laborales
- Mejora de la fidelidad visual del entorno

## Créditos y recursos

- Modelos 3D obtenidos de [Sketchfab](https://sketchfab.com)
- Audio obtenido de [Freesound](https://freesound.org)
- Documentación técnica de [Meta Horizon para Unity](https://developers.meta.com/horizon/develop/unity/)
- Proyecto desarrollado como Trabajo Fin de Máster del Máster en Industria 4.0 en la Universidad Internacional de Valencia (VIU), bajo la tutorización de Salva Aparici Martínez

## Licencia

Pendiente de definir. Al tratarse de un proyecto académico que utiliza recursos de terceros bajo sus propias licencias (Sketchfab, Freesound), conviene revisar la compatibilidad antes de publicar el repositorio bajo una licencia abierta como MIT o CC BY-NC-SA.

## Autor

**Moisés Sevilla Corrales**
GitHub: [@moisessevilla](https://github.com/moisessevilla)
