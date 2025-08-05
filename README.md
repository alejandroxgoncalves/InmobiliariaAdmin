# 🏢 InmobiliariaApp - Panel Administrativo

Este es el **panel de administración** para una aplicación de gestión inmobiliaria, desarrollado con **ASP.NET Core MVC** y **Entity Framework Core**. Permite gestionar propiedades, agentes, clientes, intereses y visitas, orientado a uso interno de la empresa.

> ⚠️ Este proyecto corresponde **solo al módulo administrador**. La parte pública para usuarios finales será desarrollada por otro integrante del equipo.

---

## 🚀 Tecnologías usadas

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Razor Pages
- Bootstrap (en las vistas)
- Visual Studio 2022

---

## 📦 Estructura de la solución

InmobiliariaApp/
├── CapaPresentacionAdmin/ → Panel de administración (UI, controllers, views)
├── CapaPresentacionInmobiliaria/ → Módulo en desarrollo para frontend público
├── Inmobiliaria.Dominio/ → Modelos de negocio
├── Inmobiliaria.Repositorios/ → Contexto y acceso a datos
├── Inmobiliaria.API/ → (Opcional/futuro) para API REST
└── InmobiliariaApp.sln → Solución principal

---

## 🛠️ Funcionalidades actuales (Admin)

- CRUD de Propiedades 🏘️
- Gestión de Agentes 👨‍💼
- Gestión de Clientes 🧑‍💼
- Registro de Visitas 📆
- Registro de Intereses 💬
- Filtros por tipo de operación, tipo de propiedad y rangos de precio

---



## ⚙️ Cómo ejecutar el proyecto

1. Clonar el repositorio:

   git clone https://github.com/alejandroxgoncalves/InmobiliariaAdmin.git

   cd InmobiliariaAdmin

2. Abrir la solución en Visual Studio.

3. Configurar la cadena de conexión en appsettings.json de Inmobiliaria.Repositorios.

4. Ejecutar las migraciones (si corresponde):

Update-Database


5. Ejecutar el proyecto.

