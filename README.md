# 🍻 Forajido Management – Backend API

Backend REST desarrollado en **ASP.NET Core (.NET 8)** para la gestión de un bar, enfocado en **productos, inventario, ventas, gastos y balance financiero**, conectado a **PostgreSQL en la nube (Neon)** y desplegado en **Railway usando Docker**.

Este proyecto nace de una **necesidad real de negocio**: administrar el bar sin depender de un POS local y poder consultar la información desde cualquier lugar.

---

## 🚀 Tecnologías utilizadas

- **ASP.NET Core Web API (.NET 8 – LTS)**
- **Entity Framework Core**
- **PostgreSQL (Neon – Cloud Database)**
- **Docker**
- **Railway (Free Tier)**
- **Swagger / OpenAPI**
- **Git & GitHub**

---

## 🧠 Arquitectura

El proyecto sigue una arquitectura clara y mantenible:

Controllers → Services → DbContext → PostgreSQL


- **Controllers**: Manejo de HTTP, rutas y códigos de respuesta
- **Services**: Lógica de negocio
- **Models**: Entidades del dominio
- **DbContext**: Acceso a datos con EF Core

Separación de responsabilidades para facilitar mantenimiento y escalabilidad.

---

## 📦 Módulos del sistema

### 🧾 Productos
- Crear, listar, actualizar y eliminar productos
- Precios de compra y venta
- Categorización

### 📊 Inventario (Stock)
- Movimientos de entrada y salida
- Cálculo de stock actual por producto
- Control de stock insuficiente

### 💰 Ventas
- Registro de ventas con múltiples productos
- Validación de stock antes de vender
- Cálculo automático del total
- Soporte para diferentes métodos de pago

### 💸 Gastos
- Registro de egresos (arriendo, insumos, servicios, etc.)
- Clasificación por categoría

### 📈 Balance
- Cálculo de ingresos, gastos y utilidad
- Resumen financiero del negocio

---

## 🔐 Seguridad y configuración

- **Credenciales sensibles fuera del código**
- Uso de **variables de entorno** en producción
- `appsettings.Development.json` excluido del repositorio
- Base de datos accesible solo mediante credenciales seguras

---

## 🧪 Pruebas

- Pruebas manuales usando archivos `.http`
- Validación de casos borde:
  - Stock insuficiente
  - Datos inválidos
  - Recursos inexistentes
- Swagger disponible para pruebas rápidas

---

## ☁️ Despliegue

El backend está desplegado en **Railway** usando **Docker**, lo que permite:

- Entorno controlado
- Build reproducible
- Despliegue gratuito
- URL pública accesible desde cualquier lugar

### Docker
El proyecto incluye un `Dockerfile` que:
- Compila la aplicación en .NET 8
- Publica el proyecto
- Ejecuta el backend en un contenedor ligero

---

## ▶️ Ejecución local

### Requisitos
- .NET SDK 8
- PostgreSQL (o conexión a Neon)

### Pasos
```bash
dotnet restore
dotnet run

