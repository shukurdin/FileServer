export default {

    isImage(fileName) {

        return /\.(jpe?g|png|gif)$/i.test(fileName);
    },

    isText(fileName) {

        return /\.txt$/i.test(fileName);
    }
}