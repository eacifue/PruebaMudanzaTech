import React, {Component} from 'react';
import { saveAs } from 'file-saver';

export default  class Posts extends Component{

    downLoadFile(data, type) {
        var blob = new Blob([data], { type: type.toString() });
        var url = window.URL.createObjectURL(blob);
        saveAs(blob,"ArchivoSalida.txt");
    }


    uploadAction(){
        var data= new FormData();
        var cedula = document.getElementById('txtCedula').value;
        var filedata = document.querySelector('input[type="file"]').files[0];
        data.append("ArchivoCargado", filedata);

        fetch("http://localhost:51304/api/Mudanzas/PostCargarArchivo/" + cedula, {

            mode: 'no-cors',
            method: 'POST',
            headers:{
                "Accept": "application/octet-stream"
            },
            body:data,
            responseType: 'blob'

        }).then(function (res) {
            console.log(res);            
            res.blob().then(function(myBlob) {
                var objectURL = URL.createObjectURL(myBlob);
                saveAs(myBlob,"ArchivoSalida.txt");         
            });
          }, function (e) {
            alert("Error submitting form!");
          });

    }




    render(){

        return(
            <form encType="multipart/form-data" action="">
                <div className="col col-md-8">
                    <input type="text" placeholder="Cedula" id="txtCedula" className="form-control"/>
                </div>
                
                <br/>
                <div className="col col-md-8">
                <input type="file" className="form-control"></input>
                </div>
                <br/>
                <div className="col col-md-8">
                <input type="button" className="btn btn-primary" value="Procesar" onClick={ this.uploadAction.bind(this)}></input>
                </div>
    
    
            </form>
    
    
        )

    }

}