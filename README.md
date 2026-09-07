# Revit Power Calculator Plugin / Плагин для расчета электрической нагрузки

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Revit](https://img.shields.io/badge/Revit-2026-orange.svg)](https://www.autodesk.com/products/revit/)

## 📖 Описание / Description

**Русский:**

Плагин для Autodesk Revit, автоматизирующий расчет электрической нагрузки на основе установленной мощности оборудования.

Плагин выполняет расчет по формуле:
Pр = Pуст * Кс

text
где:
- `Pр` - расчетная мощность (кВт)
- `Pуст` - установленная мощность (кВт)
- `Кс` - коэффициент спроса


## 🏗️ Архитектура / Architecture

Проект разделен на три части для удобства разработки и тестирования:
RevitPlugin/
├── RevitPlugin.Core/ # Бизнес-логика (не зависит от Revit API)
├── RevitPlugin.Tests/ # Тесты для Core логики
└── RevitPlugin/ # Плагин для Revit

text

## 🛠️ Технологии / Technologies

- **C#** - основной язык разработки
- **.NET Standard 2.0** - Core библиотека
- **.NET Framework 4.8** - плагин для Revit
- **Revit API 2026** - работа с Revit

## 📋 Функциональность / Features

- ✅ Автоматический сбор электрических щитов из проекта
- ✅ Расчет нагрузки по формуле Pр = Pуст * Кс
- ✅ Группировка нагрузок по категориям
- ✅ Отображение результатов в TaskDialog
- ✅ Кросс-платформенная разработка (macOS + Windows)
- ✅ Расширяемая архитектура для добавления новых функций

## 🚀 Установка / Installation

### Для пользователей:

1. Скачайте последнюю версию плагина из [Releases](https://github.com/YOUR_USERNAME/RevitPowerCalculator/releases)

2. Скопируйте файлы в папку Revit Addins:
   ```powershell
   # Windows
   copy RevitPlugin.dll %APPDATA%\Autodesk\Revit\Addins\2026\
   copy RevitPlugin.Core.dll %APPDATA%\Autodesk\Revit\Addins\2026\
   copy RevitPlugin.addin %APPDATA%\Autodesk\Revit\Addins\2026\
Перезапустите Revit

Используйте: Надстройки → Внешние инструменты → Расчет электрической нагрузки

Для разработчиков:
macOS (разработка логики):
bash
git clone git@github.com:YOUR_USERNAME/RevitPowerCalculator.git
cd RevitPowerCalculator
./build.sh  # Тестирование логики
Windows (сборка плагина):
powershell
cd Z:\RevitPlugin
dotnet build RevitPlugin\RevitPlugin.csproj -c Debug -p:DefineConstants=WINDOWS
🔧 Сборка / Build
macOS:
bash
./build.sh
Windows (Parallels):
powershell
dotnet build RevitPlugin\RevitPlugin.csproj -c Debug -p:DefineConstants=WINDOWS
📁 Структура проекта / Project Structure
text
RevitPlugin/
├── RevitPlugin.Core/
│   ├── RevitPlugin.Core.csproj
│   ├── ElectricalCalculator.cs      # Основная логика расчета
│   └── LoadData.cs                  # Модель данных
│
├── RevitPlugin.Tests/
│   ├── RevitPlugin.Tests.csproj
│   └── Program.cs                   # Тесты на macOS
│
├── RevitPlugin/
│   ├── RevitPlugin.csproj
│   └── ElectricalLoadCalculator.cs  # Интеграция с Revit API
│
├── RevitPlugin.addin                # Манифест для Revit
├── build.sh                         # Скрипт сборки на macOS
└── README.md                        # Этот файл
🔄 Рабочий процесс / Workflow
Разработка на macOS:

Пишите логику в RevitPlugin.Core

Тестируйте через RevitPlugin.Tests

Запускайте ./build.sh для проверки

Сборка на Windows (Parallels):

Откройте общую папку Z:/RevitPlugin

Выполните сборку с флагом WINDOWS

Копируйте DLL в папку Revit

Тестирование в Revit:

Перезапустите Revit

Надстройки → Внешние инструменты → Расчет электрической нагрузки

🤝 Вклад в проект / Contributing
Форкните репозиторий

Создайте ветку для новой функции (git checkout -b feature/amazing-feature)

Сделайте коммит (git commit -m 'Add amazing feature')

Запушьте ветку (git push origin feature/amazing-feature)

Откройте Pull Request

📞 Контакты / Contact
Создайте Issue в репозитории или свяжитесь с разработчиком.


