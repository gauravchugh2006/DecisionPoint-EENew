//Used for storing all constants values to be used
function Constants() {

    this.DocId = 0; 
    this.status = false;
    this.FileName = '';
    this.FileType = '';

}
Constants.prototype.SetValues = function (docid, status, filename, filetype) {
   
    var self = this.Constants();
    self.DocId = docid;
    self.status = status;
    self.FileName = filename;
    self.FileType = filetype;
}