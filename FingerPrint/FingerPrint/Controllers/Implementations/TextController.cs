using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerPrint.Models.Interfaces.TypeInterfaces;
using System.IO;
using FingerPrint;
using FingerPrint.Stores;

namespace FingerPrint.Controllers.Implementations
{
    public class TextController : ITextController<ISingleWordCountModel>
    {
        private ITextStore<ISingleWordCountModel> _textStore;
        private IGroupStore<ISingleWordCountModel> _groupStore;
        private IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> _modelFactory;

        public TextController(ITextStore<ISingleWordCountModel> textStore,
            IGroupStore<ISingleWordCountModel> groupStore,
            IModelFactory<ISingleWordCountModel, IFlexibleWordCountModel<ISingleWordCountModel>> modelFactory)
        {
            _textStore = textStore;
            _groupStore = groupStore;
            _modelFactory = modelFactory;
        }

        public ITextViewModel<ISingleWordCountModel> GetTextByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Text name must not be null or white space.");
            }
            return _textStore.GetOne(x => x.Name == name);
        }

        public List<ITextViewModel<ISingleWordCountModel>> GetTextByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("Author must not be null or white space.");
            }
            return _textStore.GetMany(x => x.Author == author).Select(x => (ITextViewModel<ISingleWordCountModel>)x).ToList();
        }

        public void CreateText(string name, TextReader input, int length, string author = null)
        {
            var model = _modelFactory.GetTextModel(name, input, length);
            if (!string.IsNullOrWhiteSpace(author))
            {
                model.SetAuthor(author);
            }
            _textStore.Add(model);
        }

        public void DeleteText(ITextViewModel<ISingleWordCountModel> model)
        {
            _textStore.Delete((ITextModel<ISingleWordCountModel>)model);
        }

        public void UpdateText(ITextViewModel<ISingleWordCountModel> model, string name = null, string author = null, bool? quotesOn = null)
        {
            var updatedModel = (ITextModel<ISingleWordCountModel>)model;
            if (!string.IsNullOrWhiteSpace(name))
            {
                updatedModel.SetName(name);
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                updatedModel.SetAuthor(author);
            }
            if (quotesOn != null)
            {
                updatedModel.SetIncludeQuotes((bool)quotesOn);
            }
            _textStore.Modify(updatedModel);
        }
    }
}
