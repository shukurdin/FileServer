import axios from 'axios';
import Utils from '../utilites/Utils';

const API_PREFIX = '/api/files/';

export default {

    getFiles() {
        return new Promise(function (resolve, reject) {
            axios.get(API_PREFIX)
                .then(response => resolve(response.data))
                .catch(error => reject(error));
        });
    },

    uploadFile(formData, onProgress) {
        return new Promise(function (resolve, reject) {
            axios.post(API_PREFIX,
                formData,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    },
                    onUploadProgress: function (progressEvent) {
                        onProgress(parseInt(Math
                            .round(progressEvent.loaded * 100 / progressEvent.total)));
                    }.bind(this)
                }
            )
                .then(response => resolve(response.data))
                .catch(error => reject(error));
        });


    },
    getFile(fileName) {

        return new Promise(function (resolve, reject) {
            axios.get(API_PREFIX + fileName, {
                responseType: 'blob'
            })
                .then(response => {

                    let reader = new FileReader();

                    reader.addEventListener("load", function () {
                        resolve(reader.result);
                    }.bind(this), false);

                    if (response.data == null) return;

                    if (Utils.isImage(fileName)) {
                        reader.readAsDataURL(response.data);
                    }
                    else if (Utils.isText(fileName))
                        reader.readAsText(response.data, "UTF-8");
                    else reject(new Error("Can't open!"));
                });
        });
    },

    removeFile(fileName) {
        return axios.delete(API_PREFIX + fileName);
    }
}