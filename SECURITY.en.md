[English](SECURITY.en.md) | [Русский](SECURITY.ru.md) | [简体中文](SECURITY.md)

# Security Policy

## Supported Versions

| Version | Supported |
| --- | --- |
| Latest Release | ✅ Security Updates |
| Older Releases | ⚠️ Critical vulnerabilities only |
| Actions builds (latest) | ✅ Security Updates |

## Reporting a Vulnerability

If you find a security vulnerability (e.g. sensitive data exposure, command injection, buffer overflow, etc.), please do **not** report it via a public Issue.

Please report it privately using the following methods:

1. Send a private message to the repository maintainer (on their GitHub account).
2. Describe the vulnerability, its scope of impact, and steps to reproduce.
3. Wait for the maintainers to confirm and establish a fix plan before disclosure.

## Security Response Process

1. **Confirmation**: Maintainers will confirm receipt of the report within 7 days.
2. **Assessment**: Assess the severity and plan the fix.
3. **Fix**: Develop a patch.
4. **Disclosure**: Once the patch is released, the maintainer will coordinate public disclosure timing with the reporter.

## Known Security Notes

- BBDown only downloads content that users have permission to access. Users must ensure their behavior is lawful and compliant.
- Users must ensure that binary dependencies (ffmpeg, mp4box, aria2c) are sourced from safe and trusted locations.
