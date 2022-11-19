using AdminPanel.Models;
using AdminPanel.Models.AdminPanel.Models;

namespace AdminPanel.Services
{
    public class TeacherService
    {
        ApplicationContext db;
        public TeacherService(ApplicationContext context)
        {
            db = context;
        }
        public List<Teachers> GetAll()
        {
            return db.Teacher.ToList();
        }
        public Teachers GetDetails(int TeacherId)
        {
            return db.Teacher.Where(x => x.Id == TeacherId).FirstOrDefault();
        }

        public int Create(Teachers teacher)
        {
            db.Teacher.Add(teacher);
            db.SaveChanges();
            return teacher.Id;
        }

        public void Delete(Teachers teacher)
        {
            db.Teacher.Remove(teacher);
            db.SaveChanges();
        }
        public int Edit(Teachers teacher)
        {
            //получить новость с таким же ID из базы. 
            var OldNews = GetDetails(teacher.Id);
            //Обновляю все поля из новых полей которые пришли
            db.Entry(OldNews).CurrentValues.SetValues(teacher);
            //сохраняю все в базе
            db.SaveChanges();
            //вернуть айди
            return teacher.Id;
        }
    }
}
