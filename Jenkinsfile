pipeline {
    agent any
    stages {
        stage('build') {
            steps {
                echo 'Build'
                sh 'dotnet build WebApi.sln'
            }
        }

        stage('publish') {
            steps {
                sh 'dotnet publish WebApi.sln -p:Configuration=release -v:q -o'
            }
        }
    }
    post { 
        always { 
            sh 'zip -r artifact.zip WebApplication2/out/'
            archiveArtifacts artifacts: 'artifact.zip'
        }
    }
}