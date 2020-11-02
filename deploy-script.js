var FtpDeploy = require("ftp-deploy");
var ftpDeploy = new FtpDeploy();

var config = {
  user: "mhyzkie0111-001", // NOTE that this was username in 1.x
  password: "Idelivery_11", // optional, prompted if none given
  host: "ftp.site4now.net",
  localRoot: "dist",
  remoteRoot: "/api-idelivery/",
  include: ["*", "**/*"], // this would upload everything except dot files
  deleteRemote: true, // delete ALL existing files at destination before uploading, if true
  forcePasv: true // Passive mode is forced (EPSV command is not sent)
};

ftpDeploy.deploy(config, function(err) {
    if (err) console.log(err)
    else console.log('deploy finished!');
});

ftpDeploy.on('uploaded', function(data) {
    console.log(data);         // same data as uploading event
});
