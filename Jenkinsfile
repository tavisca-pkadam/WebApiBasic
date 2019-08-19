pipeline {

    agent any

    parameters {
        string(name:"SOLUTION_NAME", defaultValue:"WebApi.sln", description: "Solution Name")
        string(name:"SOLUTION_DLL", defaultValue:"WebApi.dll", description: "Solution Name")

        string(name:"SONAR_PROJECT_NAME", defaultValue:"WebApi", description: "Solution Name")

        string(name:"DOCKER_IMAGE_NAME", defaultValue:"aspcore_webapplication", description: "Docker Image Name")

        text(name:"TEST_PROJ_PATH", defaultValue:"", description: "Test Project Path To .csproj file")
        string(name:"PORT_NO", defaultValue:"8989", description: "Bind Port Number")

        booleanParam(name: 'BUILD', defaultValue: false, description: 'Check To Build')
        booleanParam(name: 'TEST', defaultValue: false, description: 'Check To Test')
        booleanParam(name: 'PUBLISH', defaultValue: false, description: 'Check To Publish')
        booleanParam(name: 'DEPLOY', defaultValue: false, description: 'Check To Deploy')
        booleanParam(name: 'SONAR_ANALYSIS', defaultValue: false, description: 'Check To Sonar Analysis')
        booleanParam(name: 'DOCKER_BUILD', defaultValue: false, description: 'Check To DOCKER_BUILD')
        booleanParam(name: 'DOCKER_HUB_PUBLISH', defaultValue: false, description: 'Check To DOCKER_HUB_PUBLISH')
        booleanParam(name: 'DOCKER_RUN', defaultValue: false, description: 'Check To DOCKER_RUN')
    }

    stages {
        stage('build') {
            when {
                expression { params.BUILD || params.PUBLISH || params.TEST}
            }
            steps {
                bat '''dotnet build %SOLUTION_NAME%'''
            }
        }

        stage('publish') {
             when {
                expression { return params.PUBLISH || params.DEPLOY }
            }
            steps {
                bat '''dotnet publish %SOLUTION_NAME% -p:Configuration=release -v:q -o ../artifacts'''
            }
        }

        stage('sonar') {
             when {
                expression { return params.SONAR_ANALYSIS}
            }
            steps{
                bat '''   
                    dotnet C:\\Users\\pakadam\\Downloads\\CodeJam\\sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0\\SonarScanner.MSBuild.dll begin /k:"%SONAR_PROJECT_NAME%" /d:sonar.host.url="http://localhost:10100"  /d:sonar.login="5d44d8322a7ad225ff08a0d85ecc43df60958d01"
                    dotnet  build
                    dotnet C:\\Users\\pakadam\\Downloads\\CodeJam\\sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0\\SonarScanner.MSBuild.dll end  /d:sonar.login="5d44d8322a7ad225ff08a0d85ecc43df60958d01"
                '''
            }     
        }

        stage('sonar_plugin') {
             when {
                expression { return params.SONAR_ANALYSIS}
            }
            steps{
                
                script {
                  scannerHome = tool 'sonar_ms'
                }
                withSonarQubeEnv('sonar_qube') {
                  bat "%scannerHome%/bin/sonar-scanner"
                }
            }     
        }


         stage('Docker build') {
            when {
                  expression {return params.DOCKER_BUILD}
            }
             steps{
                bat 'docker build -t %DOCKER_IMAGE_NAME% .'
             }
        }

        stage('UpdateDockerRepo') {
            when {
                  expression {return params.DOCKER_HUB_PUBLISH}
            }
            steps {
                bat ''' docker tag %DOCKER_IMAGE_NAME%:latest subtleparesh/%DOCKER_IMAGE_NAME% 
                        docker push subtleparesh/%DOCKER_IMAGE_NAME%:latest'''
            }
        }
         stage('run docker image'){
             when {
                  expression {return params.DOCKER_RUN}
            }
            steps{
                echo 'run the image'
                bat 'docker run -p %PORT_NO%:80 -e SOLUTION_DLL=%SOLUTION_DLL%  %DOCKER_IMAGE_NAME%'
            }
        }

        
    }
  
}