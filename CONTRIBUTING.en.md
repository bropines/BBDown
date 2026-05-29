[English](CONTRIBUTING.en.md) | [Русский](CONTRIBUTING.ru.md) | [简体中文](CONTRIBUTING.md)

# Contributing Guide

Thank you for your interest in contributing to the BBDown project! Please read this guide before submitting your contribution.

## Development Environment

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download) (or .NET 10.0 SDK depending on version branch)
- Supported operating systems: Windows / Linux / macOS

## Building

```bash
# Restore dependencies
dotnet restore

# Build (Debug)
dotnet build

# Build (Release)
dotnet build -c Release

# Publish single-file binary (example: win-x64)
dotnet publish BBDown -r win-x64 -c Release
```

## Code Style

- Indentation: 4 spaces
- Encoding: UTF-8
- Line endings: Cross-platform, handled automatically by Git
- Follow configurations specified in `.editorconfig`

## Branching Strategy (Mandatory)

> ⚠️ **The `master` branch is protected. Direct pushes are disabled.** All changes must be merged via Pull Requests.

### Branch Naming Convention

| Prefix | Purpose | Example |
|------|------|------|
| `feature/` | Development of a new feature | `feature/drm-auto-fetch` |
| `fix/` | Bug fixes | `fix/muxer-directory-creation` |
| `refactor/` | Code refactorings | `refactor/split-program-methods` |
| `docs/` | Documentation updates only | `docs/api-server-guide` |
| `deps/` | Dependency upgrades | `deps/protobuf-3-35` |

### Development Workflow

```bash
# 1. Create a branch from the latest master branch
git checkout master
git pull origin master
git checkout -b feature/my-feature

# 2. Develop and commit (adhere to Commit Conventions below)
git add .
git commit -m "feat: add auto cookie refresh"

# 3. Push to the remote repository
git push origin feature/my-feature

# 4. Open a Pull Request on GitHub and await review
# 5. Once CI checks pass and at least 1 person approves, a maintainer will merge it into master
```

### Commit Message Conventions

Adhere to the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) standard:

```
<type>(<scope>): <description>

[optional body]

[optional footer]
```

| Type | Purpose |
|------|------|
| `feat` | A new feature |
| `fix` | A bug fix |
| `refactor` | A code change that neither fixes a bug nor adds a feature |
| `perf` | A code change that improves performance |
| `docs` | Documentation only changes |
| `deps` | Updates dependencies |
| `test` | Adding missing tests or correcting existing tests |
| `chore` | Changes to the build process or auxiliary tools |

Example:
```
feat(drm): add auto key fetch from WVD device

Previously users had to manually provide --key and --kid.
Now the tool attempts to extract keys automatically when
a device.wvd file is present.

Closes #123
```

## Submitting an Issue

- Please search existing issues before submitting to avoid duplicates.
- For bug reports, please use the Bug Report template and attach `--debug` logs.
- For feature requests, please use the Feature Request template.

## Submitting a Pull Request

1. Fork the repository and create your branch: `git checkout -b feature/fooBar`
2. Modify the code and ensure `dotnet build` passes with no errors.
3. Update related documents if necessary (README, CHANGELOG, etc.).
4. Commit and push to your fork: `git push origin feature/fooBar`
5. Open a Pull Request on GitHub and fill out the PR template.

## PR Review Principles

- **Atomicity**: A PR should do one thing.
- **Backward Compatibility**: Do not break existing CLI parameters and configuration file formats.
- **Document Synchronization**: Keep documentation updated with code changes.
- **Build Checks**: `dotnet build` must pass in the CI workflow (0 Errors).
- **Review Requirements**: Non-documentation PRs require approval from at least 1 reviewer.
