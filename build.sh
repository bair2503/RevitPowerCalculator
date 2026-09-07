#!/bin/bash

echo "🔨 Сборка Revit плагина..."

# Сборка Core проекта
echo "📦 Сборка Core проекта..."
cd RevitPlugin.Core
dotnet build -c Release
cd ..

# Запуск тестов
echo "🧪 Запуск тестов..."
dotnet run --project RevitPlugin.Tests/RevitPlugin.Tests.csproj

# Сборка плагина (на macOS - тестовый режим)
echo ""
echo "📦 Сборка плагина (тестовый режим на macOS)..."
cd RevitPlugin
dotnet build -c Debug
cd ..

echo ""
echo "✅ Готово! Core логика протестирована."
echo ""
echo "📌 Для сборки плагина для Revit (в Parallels):"
echo "   1. Откройте Parallels Desktop"
echo "   2. В Windows откройте командную строку или PowerShell"
echo "   3. Перейдите в общую папку с проектом (обычно Z:/RevitPlugin)"
echo "   4. Выполните: dotnet build RevitPlugin/RevitPlugin.csproj -c Debug -p:DefineConstants=WINDOWS"
echo ""
echo "📌 Для установки плагина в Revit:"
echo "   Скопируйте RevitPlugin/bin/Debug/net48/RevitPlugin.dll в папку:"
echo "   %APPDATA%/Autodesk/Revit/Addins/2024/"
echo "   И скопируйте RevitPlugin.addin в ту же папку"
