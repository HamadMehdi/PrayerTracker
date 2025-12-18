# 🕌 Prayer Tracker – A Spiritual Accountability Platform  
*A full-stack ASP.NET Core web app designed to help individuals and communities track, manage, and reflect on their spiritual practices with purpose and consistency.*  

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0-purple)  
![Blazor](https://img.shields.io/badge/Blazor-Server-blue)  
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red)  
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-7952B3)  
![License](https://img.shields.io/badge/License-MIT-green)  

---

## 🌟 Features  

### 📊 **Dashboard & Tracking**  
- ✅ **Daily Prayer Logging** – Mark prayers with timestamps and statuses  
- 📈 **Progress Visualization** – Streaks, consistency percentages, and tier-based achievements  
- 🏆 **Gamified Tiers** – Earn XP and unlock levels (Silver, Gold, Platinum)  
- ⚡ **Quick Actions** – One-click prayer completion and session management  

### 📑 **Report Generation**  
- 📅 **Custom Reports** – Filter by date range, prayer type, and category  
- 📄 **Multiple Formats** – Generate reports in PDF, Excel, CSV, or on-screen view  
- 📊 **Analytics** – Weekly/Monthly summaries and engagement insights  

### 📚 **Content Management**  
- 🎥 **Multimedia Support** – Upload and manage text, video, and tutorial content  
- 🏷️ **Categorization** – Organize content by type, difficulty, and audience  
- 👁️ **Live Preview** – Preview content before publishing  

### 👥 **User Management**  
- 🔐 **Role-Based Access** – Admins, Teachers, and General Users  
- 📝 **User Profiles** – Personalized dashboards and progress tracking  
- 🛡️ **Secure Authentication** – Login, signup, and password management  

### 🎨 **UI/UX Highlights**  
- 📱 **Fully Responsive** – Works on mobile, tablet, and desktop  
- 🎯 **Clean & Intuitive Design** – Built with Bootstrap 5 and custom CSS  
- 📋 **Interactive Components** – Blazor-powered dynamic UI  

---

## 🛠️ Tech Stack  

| Layer          | Technology |
|----------------|------------|
| **Backend**    | ASP.NET Core 6.0 MVC, Entity Framework Core, LINQ |
| **Frontend**   | Blazor Server, Razor Pages, Bootstrap 5, Custom CSS |
| **Database**   | SQL Server 2022 |
| **Authentication** | ASP.NET Core Identity, Role-Based Authorization |
| **Reporting**  | PDFSharp, ClosedXML, CsvHelper |
| **Deployment** | IIS, Docker (optional) |

---

## 📸 Screenshots  

| Dashboard | Prayer Logging | Report Generator |
|-----------|----------------|------------------|
| ![Dashboard](https://via.placeholder.com/400x250/6c757d/fff?text=Dashboard+View) | ![Prayer Log](https://via.placeholder.com/400x250/0d6efd/fff?text=Prayer+Logging) | ![Report Gen](https://via.placeholder.com/400x250/198754/fff?text=Report+Generator) |

| Content Management | User Management | Mobile View |
|--------------------|-----------------|-------------|
| ![Content](https://via.placeholder.com/400x250/ffc107/000?text=Content+Manager) | ![Users](https://via.placeholder.com/400x250/dc3545/fff?text=User+Roles) | ![Mobile](https://via.placeholder.com/400x250/6f42c1/fff?text=Mobile+View) |

---

## 🚀 Getting Started  

### Prerequisites  
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)  
- [SQL Server 2019+](https://www.microsoft.com/sql-server)  
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)  

### Installation  
1. **Clone the repository**  
   ```bash
   git clone https://github.com/your-username/prayer-tracker.git
   cd prayer-tracker
   ```

2. **Configure the database**  
   - Update `appsettings.json` with your SQL Server connection string  
   - Run migrations:  
   ```bash
   dotnet ef database update
   ```

3. **Run the application**  
   ```bash
   dotnet run
   ```
   Open `https://localhost:5001` in your browser.

4. **Default Admin Login**  
   - Username: `admin`  
   - Password: `Admin@123` (change after first login)

---

## 📁 Project Structure  

```
PrayerTracker/
├── Controllers/          # MVC Controllers
├── Data/                # Entity Framework Models & Migrations
├── Pages/               # Razor Pages & Blazor Components
├── Services/            # Business Logic & Reporting
├── Views/               # Razor Views
├── wwwroot/             # Static Files (CSS, JS, Images)
└── appsettings.json     # Configuration
```

---

## 🧠 Why I Built This  

As a developer with a passion for meaningful technology, I wanted to create a tool that blends **spiritual discipline with digital convenience**. Prayer Tracker is more than an app—it’s a platform for accountability, reflection, and community. It reflects my belief that technology should serve human purpose, not replace it.

---

## 🔮 Future Enhancements  

- [ ] **Mobile App** – React Native or .NET MAUI version  
- [ ] **Community Features** – Group prayers and shared goals  
- [ ] **Advanced Analytics** – Prayer habit insights and recommendations  
- [ ] **Multilingual Support** – Urdu, Arabic, and more  
- [ ] **Cloud Sync** – Cross-device progress synchronization  

---

## 🤝 Contributing  

Contributions are welcome!  
1. Fork the repository  
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)  
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)  
4. Push to the branch (`git push origin feature/AmazingFeature`)  
5. Open a Pull Request  

---

## 📄 License  

This project is licensed under the MIT License – see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author  

**Muhammad Usman**  
- 🌐 [Portfolio](https://your-portfolio-link.com)  
- 💼 [LinkedIn](https://linkedin.com/in/your-profile)  
- 🐙 [GitHub](https://github.com/your-username)  

> *“Code with purpose, build with heart.”*  

---

## 🙏 Acknowledgments  

- Icons by [Font Awesome](https://fontawesome.com)  
- UI Inspiration from [AdminLTE](https://adminlte.io)  
- The .NET and Blazor communities for endless learning resources  

---

**🌟 If you find this project helpful, give it a star on GitHub!**  

---

*Built with ❤️ in Pakistan 🇵🇰*  

---
**Tags:** `#DotNetDeveloper` `#AspNetCore` `#Blazor` `#EntityFramework` `#SQLServer` `#FullStackDeveloper` `#CSharp` `#PrayerTracker` `#TechForGood` `#JuniorDeveloper` `#SoftwareEngineering` `#PakistanTech` `#EntryLevelJobs` `#CodeNewbie` `#DevCommunity` `#GamifiedApps` `#SpiritualTech`

---

### 📬 Contact  
For questions, suggestions, or collaborations, feel free to reach out:  
📧 Email: your.email@example.com  
🐦 Twitter: [@yourhandle](https://twitter.com/yourhandle)  

---

*May your code compile, your tests pass, and your prayers be answered.* 🕌✨
