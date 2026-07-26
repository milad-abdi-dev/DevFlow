# DevFlow 🚀

> A workflow and task management platform built to practice modern backend architecture with .NET.
DevFlow is a portfolio project focused on building production-style software rather than just implementing CRUD APIs. The project follows Domain-Driven Design, Clean Architecture, CQRS, and Event-Driven principles while evolving through Event Storming and iterative development.The project is heavily inspired by platforms like Jira, Linear, Trello, and GitHub Projects while focusing on clean architecture, modularity, and maintainability.
The project is heavily inspired by platforms like Jira, Linear, Trello, and GitHub Projects while focusing on clean architecture, modularity, and maintainability.

---

## 🎯 Goals

- Build software using real-world architecture
- Practice Modular Monolith architecture and Domain-Driven Design
- Apply CQRS and Event-Driven design
- Improve testing and maintainability
- Learn how complex business workflows evolve over time

---

## 📚 Features

- ⚒👷‍♂️ Workspaces
- ⬜ Projects
- ⬜ Workflows
- ⬜ Tasks
- ⬜ Workflow Execution
- ⬜ Assignments
- ⬜ Notifications
- ⬜ Activity History
- ⬜ Comments
- 🚧 More features will be added incrementally

---

## 🏗️ Architecture

DevFlow is being built using a **Modular Monolith Architecture** combined with principles from:

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- Vertical Slice Architecture
- Event-Driven Design
- Repository Pattern
- Result Pattern

Each module is independently organized into:

- Domain
- Application
- Infrastructure
- Presentation

The goal is to achieve strong modular boundaries while maintaining the simplicity and deployment advantages of a monolithic system.

---

## 📦 Solution Structure

```txt
src/
│
├── API/
│
├── Common/
│
├── Modules/
│   ├── Identity/
│   └── .../
│
tests/
│
docs/
```

---

## 🛠️ Technologies

### Backend

- .NET 10
- ASP.NET Core
- PostgreSQL
- Entity Framework Core
- Docker & Docker Compose
- MediatR
- MassTransit
- Redis

### Infrastructure

- Docker
- Docker Compose

### Testing

- xUnit
- FluentAssertions
---

# 🧪 Engineering Practices

The project aims to follow professional engineering standards including:

- Unit Testing
- Integration Testing
- Architecture Testing
- Structured Logging
- CI/CD Pipelines
- Dockerized Development Environment
- Documentation & ADRs

---

# 🚧 Project Status

DevFlow is currently under active development.

The project is in the foundational architecture phase where the core solution structure and module boundaries are being established.

---

# 🤝 Contributing

This project is currently a personal learning and portfolio project, but feedback, suggestions, and discussions are always welcome.

---

# 📄 License

This project is licensed under the MIT License.
