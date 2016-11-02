using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces;
using FingerPrint.Models.Interfaces.TypeInterfaces;

namespace FingerPrint.Stores
{
    public class GroupStore : IGroupStore
    {
        private FingerprintV2Entities _db;
        private IModelFactory _modelFactory;

        public GroupStore(FingerprintV2Entities db, IModelFactory modelFactory)
        {
            _db = db;
            _modelFactory = modelFactory;    
        }

        public void Add(IGroupModel model)
        {
            throw new NotImplementedException();
            if (_db.Groups.Any(x => x.Name == model.GetName()))
            {
                throw new ArgumentException($"Cannot add model since a model already exists in the database with name {model.GetName()}.");
            }
            List<Text> texts = new List<Text>();
            List<Group> groups = new List<Group>();
            foreach (var item in model.GetMembers())
            {
                if (item is ITextModel)
                {
                    Text text = _db.Texts.FirstOrDefault(x => x.Name == item.GetName());
                    if (text == null)
                    {
                        throw new ArgumentException($"Group model has members that do not exist in the database: text {text.Name}.");
                    }
                    texts.Add(text);
                }
                else
                {
                    Group group = _db.Groups.FirstOrDefault(x => x.Name == item.GetName());

                }

            }
        }

        public void Delete(IGroupModel model)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGroupModel> GetMany(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IGroupModel GetOne(Expression<Func<Group, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public IGroupModel ModifyName(IGroupModel model, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
