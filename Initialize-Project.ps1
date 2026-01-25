<#
.SYNOPSIS
    Interactive script to create a new project from the RevitAddInModularTemplate.

.DESCRIPTION
    This script guides you step-by-step through creating a new project by copying
    the template and renaming all occurrences of "RevitAddInModularTemplate" to
    your chosen project name. The new project is created in a sibling directory.

.EXAMPLE
    .\Initialize-Project.ps1

.NOTES
    Run this script from the repository root directory.
    The new project will be created next to this template folder.
#>

$ErrorActionPreference = "Stop"
$OldName = "RevitAddInModularTemplate"

# Get the script's directory (template root)
$TemplateRoot = $PSScriptRoot
if (-not $TemplateRoot) {
    $TemplateRoot = Get-Location
}

# Parent directory where new project will be created
$ParentDir = Split-Path $TemplateRoot -Parent

# Function to convert PascalCase to lowercase-dash
function Convert-ToKebabCase {
    param([string]$Name)
    # Insert dash before each uppercase letter (except first), then lowercase
    $result = $Name -creplace '([A-Z])', '-$1'
    $result = $result.TrimStart('-').ToLower()
    return $result
}

function Write-Header {
    param([string]$Text)
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  $Text" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
}

function Write-Step {
    param([int]$Number, [int]$Total, [string]$Text)
    Write-Host ""
    Write-Host "[$Number/$Total] $Text" -ForegroundColor Yellow
    Write-Host ("-" * 40) -ForegroundColor DarkGray
}

function Prompt-Continue {
    param([string]$Message = "Press Enter to continue or 'q' to quit...")
    Write-Host ""
    $input = Read-Host $Message
    if ($input -eq 'q' -or $input -eq 'Q') {
        Write-Host "Cancelled by user." -ForegroundColor Red
        exit 0
    }
}

# Clear screen and show welcome
Clear-Host
Write-Header "Revit Add-In Template Setup"

Write-Host "  Welcome! This script will create a new project from the template." -ForegroundColor White
Write-Host ""
Write-Host "  Template name: " -NoNewline -ForegroundColor Gray
Write-Host "$OldName" -ForegroundColor Yellow
Write-Host "  Template location: " -NoNewline -ForegroundColor Gray
Write-Host "$TemplateRoot" -ForegroundColor Gray
Write-Host "  New project will be created in: " -NoNewline -ForegroundColor Gray
Write-Host "$ParentDir" -ForegroundColor Gray
Write-Host ""

Prompt-Continue

# Step 1: Get project name
Write-Step -Number 1 -Total 8 -Text "Choose Your Project Name"

Write-Host "  Your project name should be:" -ForegroundColor White
Write-Host "    - PascalCase (e.g., MyRevitAddin, AcmeTools)" -ForegroundColor Gray
Write-Host "    - Start with an uppercase letter" -ForegroundColor Gray
Write-Host "    - Alphanumeric characters only (no spaces or symbols)" -ForegroundColor Gray
Write-Host ""

$NewName = $null
while (-not $NewName) {
    $input = Read-Host "  Enter your project name"

    if ([string]::IsNullOrWhiteSpace($input)) {
        Write-Host "  Project name cannot be empty." -ForegroundColor Red
        continue
    }

    if ($input -notmatch "^[A-Z][a-zA-Z0-9]*$") {
        Write-Host "  Invalid name. Must be PascalCase (e.g., MyRevitAddin)." -ForegroundColor Red
        continue
    }

    if ($input -eq $OldName) {
        Write-Host "  That's the current name. Please choose a different name." -ForegroundColor Red
        continue
    }

    $NewName = $input
}

# Calculate folder name (kebab-case)
$FolderName = Convert-ToKebabCase -Name $NewName
$NewProjectRoot = Join-Path $ParentDir $FolderName

Write-Host ""
Write-Host "  Project name: " -NoNewline -ForegroundColor Gray
Write-Host "$NewName" -ForegroundColor Green
Write-Host "  Folder name: " -NoNewline -ForegroundColor Gray
Write-Host "$FolderName" -ForegroundColor Green
Write-Host "  Full path: " -NoNewline -ForegroundColor Gray
Write-Host "$NewProjectRoot" -ForegroundColor Green

# Check if target folder already exists
if (Test-Path $NewProjectRoot) {
    Write-Host ""
    Write-Host "  Error: Folder '$FolderName' already exists!" -ForegroundColor Red
    Write-Host "  Please choose a different name or delete the existing folder." -ForegroundColor Red
    exit 1
}

# Step 2: Show what will be renamed
Write-Step -Number 2 -Total 8 -Text "Review Changes"

Write-Host "  A new project will be created at:" -ForegroundColor White
Write-Host "    $NewProjectRoot" -ForegroundColor Cyan
Write-Host ""
Write-Host "  The following will be renamed:" -ForegroundColor White
Write-Host ""
Write-Host "  Folders:" -ForegroundColor Yellow
Write-Host "    $OldName/                -> $NewName/" -ForegroundColor Gray
Write-Host "    $OldName.Core/           -> $NewName.Core/" -ForegroundColor Gray
Write-Host "    $OldName.UI/             -> $NewName.UI/" -ForegroundColor Gray
Write-Host "    $OldName.Res/            -> $NewName.Res/" -ForegroundColor Gray
Write-Host ""
Write-Host "  Files:" -ForegroundColor Yellow
Write-Host "    $OldName.slnx            -> $NewName.slnx" -ForegroundColor Gray
Write-Host "    $OldName.csproj          -> $NewName.csproj" -ForegroundColor Gray
Write-Host "    $OldName.addin           -> $NewName.addin" -ForegroundColor Gray
Write-Host "    (and all other project files)" -ForegroundColor DarkGray
Write-Host ""
Write-Host "  Namespaces & References:" -ForegroundColor Yellow
Write-Host "    namespace $OldName -> namespace $NewName" -ForegroundColor Gray
Write-Host "    (in all .cs, .csproj, .addin files)" -ForegroundColor DarkGray
Write-Host ""
Write-Host "  A new unique GUID will be generated for your add-in." -ForegroundColor White

Write-Host ""
$confirm = $null
while ($confirm -ne 'y' -and $confirm -ne 'Y' -and $confirm -ne 'n' -and $confirm -ne 'N') {
    $confirm = Read-Host "  Proceed with these changes? (y/n)"
    if ([string]::IsNullOrWhiteSpace($confirm) -or ($confirm -ne 'y' -and $confirm -ne 'Y' -and $confirm -ne 'n' -and $confirm -ne 'N')) {
        Write-Host "  Please enter 'y' or 'n'." -ForegroundColor Yellow
    }
}
if ($confirm -eq 'n' -or $confirm -eq 'N') {
    Write-Host "  Cancelled." -ForegroundColor Red
    exit 0
}

# Step 3: Copy template to new location
Write-Step -Number 3 -Total 8 -Text "Copying Template"

Write-Host "  Copying template to: $NewProjectRoot" -ForegroundColor White
Write-Host ""

# Items to exclude from copy
$excludeItems = @(".git", "bin", "obj", ".vs", ".idea", "*.user")

# Create the destination folder
New-Item -Path $NewProjectRoot -ItemType Directory | Out-Null

# Copy files and folders, excluding specified items
Get-ChildItem -Path $TemplateRoot -Force | Where-Object {
    $item = $_
    $exclude = $false
    foreach ($pattern in $excludeItems) {
        if ($item.Name -like $pattern) {
            $exclude = $true
            break
        }
    }
    -not $exclude
} | ForEach-Object {
    Copy-Item -Path $_.FullName -Destination $NewProjectRoot -Recurse -Force
    Write-Host "    Copied: $($_.Name)" -ForegroundColor DarkGray
}

Write-Host ""
Write-Host "  Template copied successfully." -ForegroundColor Green

Prompt-Continue

# Step 4: Remove template-specific files
Write-Step -Number 4 -Total 8 -Text "Cleaning Up Template Files"

Write-Host "  Removing template-specific files and folders..." -ForegroundColor White
Write-Host ""

$itemsToRemove = @(
    ".git",
    ".github",
    ".gitignore",
    "Initialize-Project.ps1",
    "LICENSE",
    "README.md"
)

$itemsRemoved = 0
foreach ($item in $itemsToRemove) {
    $itemPath = Join-Path $NewProjectRoot $item
    if (Test-Path $itemPath) {
        Remove-Item -Path $itemPath -Recurse -Force -ErrorAction SilentlyContinue
        Write-Host "    Removed: $item" -ForegroundColor DarkGray
        $itemsRemoved++
    }
}

Write-Host ""
Write-Host "  $itemsRemoved item(s) removed." -ForegroundColor Green

Prompt-Continue

# Step 5: Replace content in files
Write-Step -Number 5 -Total 8 -Text "Updating File Contents"

Write-Host "  Replacing '$OldName' with '$NewName' in all files..." -ForegroundColor White
Write-Host ""

$fileExtensions = @("*.cs", "*.csproj", "*.slnx", "*.addin", "*.json", "*.xml", "*.md", "*.yml", "*.yaml")
$excludeDirs = @("bin", "obj", ".vs", ".idea")
$filesUpdated = 0

foreach ($ext in $fileExtensions) {
    $files = Get-ChildItem -Path $NewProjectRoot -Filter $ext -Recurse -File -ErrorAction SilentlyContinue |
        Where-Object {
            $exclude = $false
            foreach ($dir in $excludeDirs) {
                if ($_.FullName -like "*\$dir\*") {
                    $exclude = $true
                    break
                }
            }
            -not $exclude
        }

    foreach ($file in $files) {
        $content = Get-Content -Path $file.FullName -Raw -ErrorAction SilentlyContinue
        if ($content -and $content -match $OldName) {
            $newContent = $content -replace $OldName, $NewName
            Set-Content -Path $file.FullName -Value $newContent -NoNewline
            $relativePath = $file.FullName -replace [regex]::Escape($NewProjectRoot), "."
            Write-Host "    Updated: $relativePath" -ForegroundColor DarkGray
            $filesUpdated++
        }
    }
}

Write-Host ""
Write-Host "  $filesUpdated file(s) updated." -ForegroundColor Green

Prompt-Continue

# Step 6: Rename files and folders
Write-Step -Number 6 -Total 8 -Text "Renaming Files and Folders"

Write-Host "  Renaming files..." -ForegroundColor White
$filesRenamed = 0

$filesToRename = Get-ChildItem -Path $NewProjectRoot -Recurse -File -ErrorAction SilentlyContinue |
    Where-Object {
        $_.Name -like "*$OldName*" -and
        $_.FullName -notlike "*\bin\*" -and
        $_.FullName -notlike "*\obj\*"
    } |
    Sort-Object { $_.FullName.Length } -Descending

foreach ($file in $filesToRename) {
    $newFileName = $file.Name -replace $OldName, $NewName
    if ($file.Name -ne $newFileName) {
        Rename-Item -Path $file.FullName -NewName $newFileName -ErrorAction SilentlyContinue
        Write-Host "    $($file.Name) -> $newFileName" -ForegroundColor DarkGray
        $filesRenamed++
    }
}

Write-Host ""
Write-Host "  Renaming folders..." -ForegroundColor White
$foldersRenamed = 0

$foldersToRename = Get-ChildItem -Path $NewProjectRoot -Recurse -Directory -ErrorAction SilentlyContinue |
    Where-Object {
        $_.Name -like "*$OldName*" -and
        $_.FullName -notlike "*\bin\*" -and
        $_.FullName -notlike "*\obj\*"
    } |
    Sort-Object { $_.FullName.Length } -Descending

foreach ($folder in $foldersToRename) {
    $newFolderName = $folder.Name -replace $OldName, $NewName
    if ($folder.Name -ne $newFolderName -and (Test-Path $folder.FullName)) {
        Rename-Item -Path $folder.FullName -NewName $newFolderName -ErrorAction SilentlyContinue
        Write-Host "    $($folder.Name)/ -> $newFolderName/" -ForegroundColor DarkGray
        $foldersRenamed++
    }
}

Write-Host ""
Write-Host "  $filesRenamed file(s) and $foldersRenamed folder(s) renamed." -ForegroundColor Green

Prompt-Continue

# Step 7: Generate new GUID
Write-Step -Number 7 -Total 8 -Text "Generating Unique Add-in ID"

$newGuid = [guid]::NewGuid().ToString()
$addinPath = Join-Path $NewProjectRoot "addin\$NewName.addin"

if (Test-Path $addinPath) {
    $addinContent = Get-Content -Path $addinPath -Raw
    $addinContent = $addinContent -replace "a1b2c3d4-e5f6-7890-abcd-ef1234567890", $newGuid
    Set-Content -Path $addinPath -Value $addinContent -NoNewline

    Write-Host "  Your add-in has been assigned a unique identifier:" -ForegroundColor White
    Write-Host ""
    Write-Host "    $newGuid" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "  This GUID is stored in: addin\$NewName.addin" -ForegroundColor DarkGray
} else {
    Write-Host "  Warning: Could not find addin file to update GUID." -ForegroundColor Yellow
}

# Step 8: Verification scan
Write-Step -Number 8 -Total 8 -Text "Verification Scan"

Write-Host "  Scanning project for naming consistency..." -ForegroundColor White
Write-Host ""

# Count files/folders with new name
$filesWithNewName = (Get-ChildItem -Path $NewProjectRoot -Recurse -File -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -like "*$NewName*" }).Count

$foldersWithNewName = (Get-ChildItem -Path $NewProjectRoot -Recurse -Directory -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -like "*$NewName*" }).Count

# Count files containing new name in content
$filesContainingNewName = 0
foreach ($ext in $fileExtensions) {
    $files = Get-ChildItem -Path $NewProjectRoot -Filter $ext -Recurse -File -ErrorAction SilentlyContinue
    foreach ($file in $files) {
        $content = Get-Content -Path $file.FullName -Raw -ErrorAction SilentlyContinue
        if ($content -and $content -match $NewName) {
            $filesContainingNewName++
        }
    }
}

# Check for any remaining old name references
$remainingOldNameFiles = @()
$remainingOldNameContent = @()

# Check file/folder names
Get-ChildItem -Path $NewProjectRoot -Recurse -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -like "*$OldName*" } |
    ForEach-Object { $remainingOldNameFiles += $_.FullName -replace [regex]::Escape($NewProjectRoot), "." }

# Check file contents
foreach ($ext in $fileExtensions) {
    $files = Get-ChildItem -Path $NewProjectRoot -Filter $ext -Recurse -File -ErrorAction SilentlyContinue
    foreach ($file in $files) {
        $content = Get-Content -Path $file.FullName -Raw -ErrorAction SilentlyContinue
        if ($content -and $content -match $OldName) {
            $remainingOldNameContent += $file.FullName -replace [regex]::Escape($NewProjectRoot), "."
        }
    }
}

# Display results
Write-Host "  New Name Statistics:" -ForegroundColor Green
Write-Host "    Files named with '$NewName':      $filesWithNewName" -ForegroundColor Gray
Write-Host "    Folders named with '$NewName':    $foldersWithNewName" -ForegroundColor Gray
Write-Host "    Files containing '$NewName':      $filesContainingNewName" -ForegroundColor Gray
Write-Host ""

$oldNameIssues = $remainingOldNameFiles.Count + $remainingOldNameContent.Count

if ($oldNameIssues -eq 0) {
    Write-Host "  Old Name Check:" -ForegroundColor Green
    Write-Host "    No references to '$OldName' found" -ForegroundColor Gray
    Write-Host ""
    Write-Host "  All checks passed!" -ForegroundColor Green
} else {
    Write-Host "  Old Name Check:" -ForegroundColor Yellow
    if ($remainingOldNameFiles.Count -gt 0) {
        Write-Host "    Files/folders still named with '$OldName':" -ForegroundColor Yellow
        foreach ($item in $remainingOldNameFiles) {
            Write-Host "      $item" -ForegroundColor DarkYellow
        }
    }
    if ($remainingOldNameContent.Count -gt 0) {
        Write-Host "    Files still containing '$OldName':" -ForegroundColor Yellow
        foreach ($item in $remainingOldNameContent) {
            Write-Host "      $item" -ForegroundColor DarkYellow
        }
    }
    Write-Host ""
    Write-Host "  Warning: Some references to old name remain. Manual review recommended." -ForegroundColor Yellow
}

# Complete
Write-Header "Setup Complete!"

Write-Host "  Your project '$NewName' is ready!" -ForegroundColor Green
Write-Host ""
Write-Host "  Location:" -ForegroundColor Yellow
Write-Host "    $NewProjectRoot" -ForegroundColor Cyan
Write-Host ""
Write-Host "  Project Structure:" -ForegroundColor Yellow
Write-Host "    $NewName/           - Main entry point" -ForegroundColor Gray
Write-Host "    $NewName.Core/      - Commands and business logic" -ForegroundColor Gray
Write-Host "    $NewName.UI/        - Ribbon UI helpers" -ForegroundColor Gray
Write-Host "    $NewName.Res/       - Embedded resources (icons)" -ForegroundColor Gray
Write-Host ""
Write-Host "  Next Steps:" -ForegroundColor Yellow
Write-Host "    1. cd $NewProjectRoot" -ForegroundColor White
Write-Host "    2. Open $NewName.slnx in Visual Studio" -ForegroundColor White
Write-Host "    3. Build the solution (Ctrl+Shift+B)" -ForegroundColor White
Write-Host "    4. Press F5 to start debugging with Revit" -ForegroundColor White
Write-Host ""
Write-Host "  Happy coding!" -ForegroundColor Cyan
Write-Host ""
