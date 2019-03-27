//var HOST = window.location.href;

Array.prototype.last = function () { return this[this.length - 1]; }

String.prototype.trimCss = function () {

    return this.replace(/[^a-zA-Z0-9]/g, '_');
}